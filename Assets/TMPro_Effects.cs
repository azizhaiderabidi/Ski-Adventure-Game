using System.Collections;
using UnityEngine;
using TMPro;


#pragma warning disable 1591
namespace HardDev.Utility
{
    public static class TMPro_Effects
    {
        public static void TypeWriter(this TextMeshProUGUI textMesh, string text, float duration
            , float delay = 0f, string leadingChar = "", bool leadingCharBeforeDelay = false)
        {
            string writer = text;

            textMesh.text = leadingCharBeforeDelay ? leadingChar : "";

            textMesh.StartCoroutine(Type(duration / writer.Length, delay));


            IEnumerator Type(float time, float delay)
            {
                yield return new WaitForSeconds(delay);

                foreach (char c in writer)
                {
                    if (textMesh.text.Length > 0)
                    {
                        textMesh.text = textMesh.text.Substring(0, textMesh.text.Length - leadingChar.Length);
                    }

                    textMesh.text += c;
                    textMesh.text += leadingChar;

                    yield return new WaitForSeconds(time);

                }

                if (leadingChar != "")
                {
                    textMesh.text = textMesh.text.Substring(0, textMesh.text.Length - leadingChar.Length);
                }
            }
        }
    }
}