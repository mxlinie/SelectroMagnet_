using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ToonSketch
{
    public class EditorUtils
    {
        public const string version = "1.00";

        public static string GetTitle()
        {
            return "ToonSketch";
        }

        public static string GetByline()
        {
            return string.Format("© Ikonoclast [v{0}]", version);
        }

        public static string[] GetColors(int index)
        {
            string[][] colorList = new string[][] {
                new string[] {"#a40061", "#b75592", "#ececea", "#c44e55", "#8a1e04"},
                new string[] {"#74d7ec", "#ffafc7", "#fbf9f5", "#ffafc7", "#74d7ec"},
                new string[] {"#000000", "#5d5c5c", "#ffffff", "#9100ff", "#000000"},
                new string[] {"#40a545", "#aad37e", "#ffffff", "#ababab", "#000000"},
                new string[] {"#f95151", "#f9e35f", "#83d45a", "#62afdf", "#8678dd"},
                new string[] {"#f95151", "#f9e35f", "#83d45a", "#62afdf", "#8678dd"},
            };
            if (colorList.Length == 0)
                return null;
            return colorList[index % colorList.Length];
        }

        public static void Logo()
        {
            GUIStyle logoStyle = new GUIStyle(EditorStyles.inspectorDefaultMargins);
            logoStyle.alignment = TextAnchor.MiddleCenter;
            GUIStyle titleStyle = new GUIStyle(EditorStyles.whiteLargeLabel);
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.richText = true;
            Texture2D logoTex = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/ToonSketch/Core/Scripts/Editor/Images/toonsketch-logo.png", typeof(Texture2D));
            if (logoTex != null)
                GUILayout.Box(logoTex, logoStyle, GUILayout.MinHeight(100f), GUILayout.MaxHeight(100f));
            else
                GUILayout.Label(string.Format("<size=14>{0}</size>", GetTitle()), titleStyle);
        }

        public static string GetFlourish(string[] colors)
        {
            return string.Format(
                "<size=10><color={0}>♥</color><size=12><color={1}>♥</color><size=14><color={2}>♥</color></size><color={3}>♥</color></size><color={4}>♥</color></size>",
                colors[0], colors[1], colors[2], colors[3], colors[4]
            );
        }

        public static void Header(string text = "", int colors = 0)
        {
            Logo();
            GUIStyle title = new GUIStyle(EditorStyles.whiteLargeLabel);
            title.fontStyle = FontStyle.Bold;
            title.alignment = TextAnchor.MiddleCenter;
            title.richText = true;
            GUILayout.Label(
                string.Format(
                    "{0} <size=12>{1}</size> {2}",
                    GetFlourish(GetColors(colors)),
                    text,
                    GetFlourish(GetColors(colors + 1))
                ),
                title
            );
            Seperator(GetByline(), colors + 2);
            EditorGUILayout.Space();
        }

        public static void Section(string text = "", int colors = 0)
        {
            EditorGUILayout.Space();
            Seperator(text, colors);
            EditorGUILayout.Space();
        }

        public static void Seperator(string text = "", int colors = 0)
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.fontStyle = FontStyle.Bold;
            style.alignment = TextAnchor.MiddleCenter;
            style.richText = true;
            GUILayout.Label(
                string.Format(
                    "{0}{1}{2}",
                    GetFlourish(GetColors(colors)),
                    (!string.IsNullOrEmpty(text)) ? " <size=10>" + text + "</size> " : " ",
                    GetFlourish(GetColors(colors + 1))
                ),
                style
            );
        }
    }
}