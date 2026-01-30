using System.Text;

namespace Cruzeiro_Atualizar_Lista_de_PDFs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pastaRaiz = AppDomain.CurrentDomain.BaseDirectory;

            var resultado = new StringBuilder();
            resultado.AppendLine("const acervo = [");

            bool primeiroGrupo = true;

            foreach (var semestre in Directory.GetDirectories(pastaRaiz))
            {
                string nomeSemestre = Path.GetFileName(semestre);

                // Ignora outras pastas que não são semestres (opcional)
                if (!nomeSemestre.StartsWith("Semestre"))
                    continue;

                foreach (var subpasta in Directory.GetDirectories(semestre))
                {
                    string nomeSubpasta = Path.GetFileName(subpasta);

                    if (!primeiroGrupo)
                        resultado.AppendLine(",");

                    primeiroGrupo = false;

                    resultado.AppendLine("  {");
                    resultado.AppendLine($"    categoria: \"{nomeSubpasta}\",");
                    resultado.AppendLine("    livros: [");

                    var pdfs = Directory.GetFiles(subpasta, "*.pdf");

                    for (int i = 0; i < pdfs.Length; i++)
                    {
                        string pdf = pdfs[i];
                        string titulo = Path.GetFileNameWithoutExtension(pdf);
                        string nomeArquivo = Path.GetFileName(pdf);

                        string caminhoRelativo = Path.Combine(
                            nomeSemestre,
                            nomeSubpasta,
                            nomeArquivo
                        ).Replace("\\", "/");


                        titulo = titulo.Replace("Unidade 1 - ", "");
                        titulo = titulo.Replace("Unidade 2 - ", "");
                        titulo = titulo.Replace("Unidade 3 - ", "");
                        titulo = titulo.Replace("Unidade 4 - ", "");
                        titulo = titulo.Replace("Unidade 5 - ", "");
                        titulo = titulo.Replace("Unidade 6 - ", "");
                        titulo = titulo.Replace("Unidade 7 - ", "");
                        titulo = titulo.Replace("Unidade 8 - ", "");

                        resultado.Append("      { ");
                        resultado.Append($"titulo: \"{titulo}\", ");
                        resultado.Append($"arquivo: \"{caminhoRelativo}\" ");
                        resultado.Append("}");

                        if (i < pdfs.Length - 1)
                            resultado.Append(",");

                        resultado.AppendLine();
                    }

                    resultado.AppendLine("    ]");
                    resultado.AppendLine("  }");
                }
            }

            resultado.AppendLine("]");

            textBox1.Text = resultado.ToString();
        }
    }
}