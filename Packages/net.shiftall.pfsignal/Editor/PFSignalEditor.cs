using net.shiftall.pfsignal.Udon;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;

namespace net.shiftall.pfsignal.Editor
{
    [CustomEditor(typeof(PFSignal))]
    public class PFSignalEditor : UnityEditor.Editor
    {
        /// <summary>
        /// 公式ページURL
        /// </summary>
        private const string ShiftallURL = @"https://shiftall.net/";
        
        private SerializedProperty colliders,debug;
        
        /// AAChair by Kamishiro (https://github.com/AoiKamishiro/VRChatPrefabs/blob/master/Assets/00Kamishiro/AAChair/AAChair-README_JP.md) mit license
        private Texture logoTexture;

        public void OnEnable()
        {
            // \Images\PebbleFeel.png.meta
            const string HeaderImageGuid = "f211297b8c000534d9e965b2d069fe73";
            
            colliders = serializedObject.FindProperty(nameof(PFSignal.colliders));
            debug = serializedObject.FindProperty(nameof(PFSignal.debug));
            logoTexture = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(HeaderImageGuid), typeof(Texture)) as Texture;
        }

        public override void OnInspectorGUI()
        {
            if (UdonSharpGUI.DrawConvertToUdonBehaviourButton(target) || UdonSharpGUI.DrawProgramSource(target)) return;

            serializedObject.Update();
            
            DrawUILine(Color.gray);

            // ロゴ
            EditorGUILayout.Space();
            if (logoTexture != null)
            {
                float w = EditorGUIUtility.currentViewWidth;
                Rect logoRect = new Rect { width = w - 40f };
                logoRect.height = logoRect.width / 4f;
                logoRect.x = ((EditorGUIUtility.currentViewWidth - logoRect.width) * 0.5f) - 4.0f;
                logoRect.y = GUILayoutUtility.GetRect(logoRect.width, logoRect.height).y;
                GUI.DrawTexture(logoRect, logoTexture, ScaleMode.StretchToFill);
            }
            
            // コライダー設定
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(colliders, new GUIContent("Mode setting collider"));
                EditorGUILayout.HelpBox("Attach PFSignalCollider to the mode setting collider`s GameObject.", MessageType.Info);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(debug, new GUIContent("Enable debug logging"));
            }
            EditorGUILayout.EndVertical();
            
            // バージョン、Webページ
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Shiftall inc.", EditorStyles.linkLabel);
            var lastRect = GUILayoutUtility.GetLastRect();
            EditorGUIUtility.AddCursorRect(lastRect, MouseCursor.Link);
            var e = Event.current;
            if (e.type == EventType.MouseDown && lastRect.Contains(e.mousePosition))
            {
                Help.BrowseURL($@"{ShiftallURL}");
            }
            EditorGUILayout.Space();
            
            serializedObject.ApplyModifiedProperties();
            
        }

        /// https://forum.unity.com/threads/horizontal-line-in-editor-window.520812/#post-3534861
        private static void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
            r.height = thickness;
            r.y+= padding/2.0f;
            r.x-=2;
            r.width +=6;
            EditorGUI.DrawRect(r, color);
        }
    }
    
}