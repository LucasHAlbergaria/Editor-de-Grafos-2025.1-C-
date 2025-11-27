using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;


namespace Editor_de_Grafos
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();            
        }

        #region Botoes de Algoritmo do Menu
        private void BtParesOrd_Click(object sender, EventArgs e)
        {
            string msg = g.ParesOrdenados();
            MessageBox.Show(msg, "Pares Ordenados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtGrafoEuleriano_Click(object sender, EventArgs e)
        {
            if(g.IsEuleriano())
                MessageBox.Show("O grafo e Euleriano!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não e Euleriano!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtGrafoUnicursal_Click(object sender, EventArgs e)
        {
            if (g.IsUnicursal())
                MessageBox.Show("O grafo e Unicursal!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não e Unicursal!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtBuscaProfundidade_Click(object sender, EventArgs e)
        {
        }

        #endregion --------------------------------------------------------------------------------------------------

        #region botoes restantes do menu

        private void BtNovo_Click(object sender, EventArgs e)
        {
            g.limpar();
        }

        private void BtAbrir_Click(object sender, EventArgs e)
        {
            if(OPFile.ShowDialog() == DialogResult.OK)
            {
                g.abrirArquivo(OPFile.FileName);
                g.Refresh();
            }
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            if(SVFile.ShowDialog() == DialogResult.OK)
            {
                g.SalvarArquivo(SVFile.FileName);
            }
        }

        private void BtSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtPeso_Click(object sender, EventArgs e)
        {
            if(BtPeso.Checked)
            {
                BtPeso.Checked = false;
                g.setExibirPesos(false);

            }
            else
            {
                BtPeso.Checked = true;
                g.setExibirPesos(true);
            }
            g.Refresh();
        }

        private void BtPesoAleatorio_Click(object sender, EventArgs e)
        {
            if(BtPesoAleatorio.Checked)
            {
                BtPesoAleatorio.Checked = false;
                g.setPesosAleatorios(false);
            }
            else
            {
                BtPesoAleatorio.Checked = true;
                g.setPesosAleatorios(true);
            }
        }

        private void BtSobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Editor de Grafos - 2025/1\n\n" +
                "Desenvolvido por:\nLucas Henrique Albergaria\n" +
                "Profª. Roselene Costa\n" +
                "Prof. Virgilio Borges de Oliveira\n" +
                "MATRÍCULA-NOME DO ALUNO (co-autor)\n\n" + 
                "Algoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.", "Sobre o Editor de Grafos...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion --------------------------------------------------------------------------------------------------

        private void completarGrafoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.CompletarGrafo();
            g.setVerticeMarcado(null);
        }

        private void profundidadeToolStripMenuItem_Click(object sender, EventArgs e) {
            g.Profundidade(0);


        }

        private void númeroCromáticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            g.NumeroCromatico();
            
        }

        private void larguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Largura(0);
        }

        private void arvoreGeradoraMinimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.AGM(0);
        }

        private void caminhoMínimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // getAresta(vi, vj) == null => aresta não existe
            // getVertice(v) => object "Vertice"
            //getVertice(0).setCor(Color.Red);
            //getVertice(0).setRotulo("BH");
            //getAresta(0, 1).setPeso(5);

             /*string origemNome = Interaction.InputBox("Digite o nome do vértice de origem (ex: V1):", "Origem");
             if (string.IsNullOrEmpty(origemNome) || !origemNome.StartsWith("V")) return;

             string destinoNome = Interaction.InputBox("Digite o nome do vértice de destino (ex: V2):", "Destino");
             if (string.IsNullOrEmpty(destinoNome) || !destinoNome.StartsWith("V")) return;

             int origemId, destinoId;

             try
             {
                 origemId = int.Parse(origemNome.Substring(1)) - 1;
                 destinoId = int.Parse(destinoNome.Substring(1)) - 1;
             }
             catch
             {
                 MessageBox.Show("Formato inválido. Digite como: V1, V2, etc.");
                 return;
             }

             if (origemId == destinoId)
             {
                 MessageBox.Show("Origem e destino devem ser diferentes.");
                 return;
             }*/

            g.pegarorigem();


        }
    }
}
