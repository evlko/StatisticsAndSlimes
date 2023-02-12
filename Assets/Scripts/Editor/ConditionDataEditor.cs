using Models;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(ConditionData))]
    public class ConditionDataEditor : UnityEditor.Editor
    {
        private SerializedProperty _characteristicType;
        private SerializedProperty _sampleCharacteristic;
        private SerializedProperty _featureType;
        private SerializedProperty _slimeCategoricalFeature;
        private SerializedProperty _categoricalFeatureCharacteristic;
        private SerializedProperty _slimeQuantitativeFeature;
        private SerializedProperty _quantitativeFeatureCharacteristic;
        private SerializedProperty _value;
        private SerializedProperty _playgroundCharacteristicsTypes;
        private SerializedProperty _slimesCharacteristics;
        private SerializedProperty _aliveSlimes;
        private SerializedProperty _certainSlimes;

        private void OnEnable()
        {
            _playgroundCharacteristicsTypes = serializedObject.FindProperty("playgroundCharacteristicsTypes");
            _slimesCharacteristics = serializedObject.FindProperty("slimesCharacteristics");
            _characteristicType = serializedObject.FindProperty("characteristicType");
            _sampleCharacteristic = serializedObject.FindProperty("sampleCharacteristic");
            _featureType = serializedObject.FindProperty("featureType");
            _slimeCategoricalFeature = serializedObject.FindProperty("slimeCategoricalFeature");
            _categoricalFeatureCharacteristic = serializedObject.FindProperty("categoricalFeatureCharacteristic");
            _slimeQuantitativeFeature = serializedObject.FindProperty("slimeQuantitativeFeature");
            _quantitativeFeatureCharacteristic = serializedObject.FindProperty("quantitativeFeatureCharacteristic");
            _value = serializedObject.FindProperty("value");
            _aliveSlimes = serializedObject.FindProperty("aliveSlimes");
            _certainSlimes = serializedObject.FindProperty("certainSlimes");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_playgroundCharacteristicsTypes);
            switch ((PlaygroundCharacteristicsTypes)_playgroundCharacteristicsTypes.enumValueIndex)
            {
                case PlaygroundCharacteristicsTypes.Slimes:
                    EditorGUILayout.PropertyField(_slimesCharacteristics);
                    switch ((SlimesCharacteristics)_slimesCharacteristics.enumValueIndex)
                    {
                        case SlimesCharacteristics.AlivedSlimes:
                            EditorGUILayout.PropertyField(_aliveSlimes);
                            break;
                        case SlimesCharacteristics.CertainData:
                            EditorGUILayout.PropertyField(_certainSlimes);
                            break;
                    }
                    break;
                case PlaygroundCharacteristicsTypes.Numerical:
                    EditorGUILayout.PropertyField(_characteristicType);
                    switch ((CharacteristicTypes)_characteristicType.enumValueIndex)
                    {
                        case CharacteristicTypes.Sample:
                            EditorGUILayout.PropertyField(_sampleCharacteristic);
                            EditorGUILayout.PropertyField(
                                (SampleCharacteristics)_sampleCharacteristic.enumValueIndex ==
                                SampleCharacteristics.Size
                                    ? _value
                                    : _aliveSlimes);
                            break;
                        case CharacteristicTypes.Feature:
                            EditorGUILayout.PropertyField(_featureType);
                            EditorGUILayout.PropertyField(
                                (FeatureTypes)_featureType.enumValueIndex == FeatureTypes.Quantitative
                                    ? _slimeQuantitativeFeature
                                    : _slimeCategoricalFeature);
                            EditorGUILayout.PropertyField(
                                (FeatureTypes)_featureType.enumValueIndex == FeatureTypes.Quantitative
                                    ? _quantitativeFeatureCharacteristic
                                    : _categoricalFeatureCharacteristic);
                            EditorGUILayout.PropertyField(_value);
                            break;
                    }

                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}