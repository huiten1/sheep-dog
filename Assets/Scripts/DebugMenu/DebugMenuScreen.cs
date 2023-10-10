using System;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;
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
            foreach (var fieldInfo in typeof(GameData).GetFields())
            {
                var fieldContainer = Create("field-container");
                var fieldName = Create<Label>("field-name");
                fieldName.text = fieldInfo.Name;

                VisualElement fieldInput = null;
                if (fieldInfo.FieldType == typeof(int))
                {
                    // fieldInput = CreateField<IntegerField, int>(fieldInfo, gameData);
                    fieldInput = CreateTextField(fieldInfo, gameData, (newData) => int.Parse(newData));
                }
                if (fieldInfo.FieldType == typeof(float))
                {
                    fieldInput = CreateTextField(fieldInfo, gameData, (newData) => float.Parse(newData));
                }
                if (fieldInfo.FieldType == typeof(string))
                {
                    fieldInput = CreateField<TextField, string>(fieldInfo, gameData);
                    
                }
                if (fieldInfo.FieldType.IsEnum)
                {
                    var dropDown = new DropdownField();
                    dropDown.AddToClassList("field-input");
                    dropDown.value=fieldInfo.GetValue(gameData).ToString();

                    List<string> choices = new();
                    foreach (var value in Enum.GetValues(fieldInfo.FieldType))
                    {
                        choices.Add(Enum.GetName(fieldInfo.FieldType, value));
                    }
                    dropDown.choices = choices;
                    dropDown.label = fieldInfo.Name;
                    dropDown.RegisterValueChangedCallback(changedEvent =>
                        fieldInfo.SetValue(gameData, Enum.Parse(fieldInfo.FieldType, changedEvent.newValue)));
                    fieldInput = dropDown;
                    // fieldInput = CreateField<DropdownField, string>(fieldInfo,gameData);
                }

                fieldContainer.Add(fieldInput);
                scrollView.Add(fieldContainer);
            }

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

        private static TextField CreateTextField<T>(FieldInfo fieldInfo, GameData gameData , Func<string,T> setField)
        {
            var field = Create<TextField>("field-input");
            field.value = fieldInfo.GetValue(gameData).ToString();
            field.RegisterValueChangedCallback(changedEvent => fieldInfo.SetValue(gameData, setField(changedEvent.newValue)));
            field.label = fieldInfo.Name;
            return field;
        }


        private static T CreateField<T,TValueType>(FieldInfo fieldInfo, GameData gameData,string className = "field-input") where T: BaseField<TValueType>,new()
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