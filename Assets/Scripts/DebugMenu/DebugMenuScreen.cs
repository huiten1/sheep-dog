using System;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DebugMenu
{
    public class DebugMenuScreen : MonoBehaviour
    {
        [SerializeField] private UIDocument uiDocument;
        [SerializeField] private StyleSheet uiStyleSheet;
        [SerializeField] private bool show;

        private void OnValidate()
        {
            if (Application.isPlaying) return;
            if (!show)
            {
                if(uiDocument.rootVisualElement!=null)
                    uiDocument.rootVisualElement.Clear();
                return;
            }
            StartGenerate();
        }

        public void StartGenerate()
        {
            StartCoroutine(Generate());
        }

        private IEnumerator Generate()
        {
            yield return null;
            var root = uiDocument.rootVisualElement;
            root.Clear();
            
            root.styleSheets.Add(uiStyleSheet);

            var scrollView = Create<ScrollView>("scroll-view");
            var gameData = SaveManager.Load<GameData>();
            
            
            GenerateFieldsInputs(gameData,typeof(GameData).GetFields(), scrollView);

            var saveButton = Create<Button>("button");
            saveButton.clicked += () => { 
                SaveManager.Save(gameData);
                GameManager.Instance.LoadData();
                GameManager.Instance.LoadMain();
                
            };
            saveButton.text = "Save";
            
            var closeButton = Create<Button>("button");
            closeButton.clicked += () =>
            {
                root.Clear();
            };
            closeButton.text = "Close";

            var buttonsContainer = Create("buttons-container");
            buttonsContainer.Add(saveButton);
            buttonsContainer.Add(closeButton);
            root.Add(buttonsContainer);
            root.Add(scrollView);
        }

        private void GenerateFieldsInputs<T>(T data,FieldInfo[] fields, VisualElement container)
        {
            foreach (var fieldInfo in fields)
            {
                var fieldContainer = Create("field-container");
                VisualElement fieldInput = null;
                if (fieldInfo.FieldType == typeof(int))
                {
                    // fieldInput = CreateField<IntegerField, int>(fieldInfo, gameData);
                    fieldInput = CreateTextField(fieldInfo, data, int.Parse);
                }

                if (fieldInfo.FieldType == typeof(float))
                {
                    fieldInput = CreateTextField(fieldInfo, data, float.Parse);
                }

                if (fieldInfo.FieldType == typeof(string))
                {
                    fieldInput = CreateField<TextField, string,T>(fieldInfo, data);
                }

                if (fieldInfo.FieldType == typeof(Vector3))
                {
                    fieldInput = CreateField<Vector3Field, Vector3, T>(fieldInfo, data);
                }

                if (fieldInfo.FieldType.IsEnum)
                {
                    var dropDown = new DropdownField();
                    dropDown.AddToClassList("field-input");
                    dropDown.value = fieldInfo.GetValue(data).ToString();

                    List<string> choices = new();
                    foreach (var value in Enum.GetValues(fieldInfo.FieldType))
                    {
                        choices.Add(Enum.GetName(fieldInfo.FieldType, value));
                    }

                    dropDown.choices = choices;
                    dropDown.label = fieldInfo.Name;
                    dropDown.RegisterValueChangedCallback(changedEvent =>
                        fieldInfo.SetValue(data, Enum.Parse(fieldInfo.FieldType, changedEvent.newValue)));
                    fieldInput = dropDown;
                }

                if (fieldInfo.FieldType.IsClass)
                {
                    fieldContainer = Create("class-container");
                    var className = Create<Label>("class-name");
                    className.text = fieldInfo.FieldType.Name;
                    fieldContainer.Add(className);
;                   GenerateFieldsInputs(fieldInfo.GetValue(data),fieldInfo.GetType().GetFields(),fieldContainer);
                }
                
                Debug.Log(fieldInfo.Name);

                fieldContainer.Add(fieldInput);
                container.Add(fieldContainer);
            }
        }

        private static TextField CreateTextField<T,TDataType>(FieldInfo fieldInfo, TDataType gameData , Func<string,T> setField)
        {
            var field = Create<TextField>("field-input");
            field.value = fieldInfo.GetValue(gameData).ToString();
            field.RegisterValueChangedCallback(changedEvent => fieldInfo.SetValue(gameData, setField(changedEvent.newValue)));
            field.label = fieldInfo.Name;
            return field;
        }


        private static T CreateField<T,TValueType,TDataType>(FieldInfo fieldInfo, TDataType gameData,string className = "field-input") where T: BaseField<TValueType>,new()
        {
            var field = Create<T>(className);
            field.value = (TValueType)fieldInfo.GetValue(gameData);
            field.RegisterValueChangedCallback(changedEvent => fieldInfo.SetValue(gameData, changedEvent.newValue));
            field.label = fieldInfo.Name;
            return field;
        }

        private VisualElement Create(string className)
        {
            return Create<VisualElement>(className);
        }

        private static T Create<T>(string className) where T : VisualElement, new()
        {
            var element = new T();
            element.AddToClassList(className);
            return element;
        }
    }
}