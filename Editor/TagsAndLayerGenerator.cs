using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Windows;

namespace BarelyAStudio.TagsAndLayersGenerator.Editor
{
    public class TagsAndLayerGenerator
    {
        [InitializeOnLoadMethod]
        private static void OnInitLoad()
        {
            EditorApplication.projectChanged += GenerateCode;
            CompilationPipeline.compilationStarted += CompilationPipeline_compilationStarted;
        }

        private static void CompilationPipeline_compilationStarted(object obj)
        {
            GenerateCode();
        }

        [MenuItem("GenerateCode/Tags And Layer")]
        private static void GenerateCode()
        {
            StringBuilder stringBuilder = new StringBuilder();

            var layers = InternalEditorUtility.layers;
            var tags = InternalEditorUtility.tags;

            stringBuilder.AppendLine("//This is a generated file. Changes will be removed on the next compilation");
            stringBuilder.AppendLine($"namespace {TagsAndLayersGeneratorSettings.GetOrCreateSettings().@namespace}");
            stringBuilder.AppendLine("{");
            {
                stringBuilder.AppendLine($"\tpublic struct Layers");
                stringBuilder.AppendLine("\t{");
                for (int i = 0; i < layers.Length; i++)
                {
                    stringBuilder.AppendLine($"\t\tpublic const int {layers[i].ToUpper().Replace(" ", "_")} = {LayerMask.NameToLayer(layers[i])};");
                }
                stringBuilder.AppendLine("\t}");

                stringBuilder.AppendLine($"\tpublic struct Tags");
                stringBuilder.AppendLine("\t{");
                for (int i = 0; i < tags.Length; i++)
                {
                    stringBuilder.AppendLine($"\t\tpublic const string {tags[i].ToUpper().Replace(" ", "_")} = \"{tags[i]}\";");
                }
                stringBuilder.AppendLine("\t}");
            }
            stringBuilder.AppendLine("}");

            var bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());

            if (File.Exists(TagsAndLayersGeneratorSettings.GetOrCreateSettings().fileName))
            {
                var readBytes = File.ReadAllBytes(TagsAndLayersGeneratorSettings.GetOrCreateSettings().fileName);

                if (!readBytes.SequenceEqual(bytes))
                {
                    WriteToFile(bytes);
                }
            }
            else
            {
                WriteToFile(bytes);
            }
        }

        private static void WriteToFile(byte[] bytes)
        {
            Debug.Log($"Generating \"{TagsAndLayersGeneratorSettings.GetOrCreateSettings().fileName}\"");
            File.WriteAllBytes(TagsAndLayersGeneratorSettings.GetOrCreateSettings().fileName, bytes);
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}