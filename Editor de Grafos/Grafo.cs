using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Editor_de_Grafos
{
    public class Grafo : GrafoBase, iGrafo
    { 

        public bool[] visitado;
      
        public void AGM(int v)
        {
            int soma = 0;
            int n = matAdj.GetLength(0);
            bool[] visitado = new bool[n];
            int[] chave = new int[n]; // menor peso até o vértice
            int[] pai = new int[n];   // guarda de onde veio cada vértice

            for (int i = 0; i < n; i++)
            {
                chave[i] = int.MaxValue;
                pai[i] = -1;
                visitado[i] = false;
            }

            chave[0] = 0; // começa pelo vértice 0

            for (int count = 0; count < n - 1; count++)
            {
                // Escolhe o vértice não visitado com menor chave
                int u = -1;
                int min = int.MaxValue;

                for (int i = 0; i < n; i++)
                {
                    if (!visitado[i] && chave[i] < min)
                    {
                        min = chave[i];
                        u = i;
                    }
                }

                if (u == -1) break; // se não há vértice válido, para

                // Marca como visitado e pinta o vértice
                visitado[u] = true;
                getVertice(u).setCor(Color.Pink);  // 👉 pinta o vértice na árvore
                Thread.Sleep(300);                 // animação de cor

                // Se não for o vértice inicial, pinta a aresta usada
                if (pai[u] != -1)
                {
                    Aresta aresta = getAresta(u, pai[u]) ?? getAresta(pai[u], u);
                    if (aresta != null)
                    {
                        aresta.setCor(Color.Orange); // 👉 pinta a aresta da AGM
                        Thread.Sleep(300);           // animação de cor
                    }
                }

                // Atualiza vizinhos
         
                for (int ve = 0; ve < n; ve++)
                {
                    Aresta a = getAresta(u, ve) ?? getAresta(ve, u);
                    if (a != null && !visitado[ve] && a.getPeso() < chave[ve])
                    {
                        chave[ve] = a.getPeso();
                        soma += a.getPeso();
                        pai[ve] = u;
                    }
                }
                
            }
            MessageBox.Show($"Soma dos pesos é: {soma}");
        }
       

        public void CaminhoMinimo(int origem, int destino)
        {
            int n = matAdj.GetLength(0);  
            int[] dist = new int[n];
            int[] anterior = new int[n];
            bool[] visitado = new bool[n];
            int somaPesos = 0; 

            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                anterior[i] = -1;
                visitado[i] = false;
            }

            dist[origem] = 0;

            for (int count = 0; count < n - 1; count++)
            {
                int u = -1;
                int min = int.MaxValue;

                for (int i = 0; i < n; i++)
                {
                    if (!visitado[i] && dist[i] < min)
                    {
                        min = dist[i];
                        u = i;
                    }
                }

                if (u == -1) break;

                visitado[u] = true;
                getVertice(u).setCor(Color.Pink);
                Thread.Sleep(300);

                for (int v = 0; v < n; v++)
                {
                    Aresta a = getAresta(u, v);
                    if (a != null && !visitado[v])
                    {
                        int peso = a.getPeso();
                        if (dist[u] + peso < dist[v])
                        {
                            dist[v] = dist[u] + peso;
                            anterior[v] = u;
                        }
                    }
                }
            }

            // 👉 Traça o caminho mínimo da origem até o destino
            if (anterior[destino] != -1)
            {
                int atual = destino;
                while (atual != origem)
                {
                    int pai = anterior[atual];
                    Aresta a = getAresta(atual, pai) ?? getAresta(pai, atual);
                    if (a != null)
                    {
                        a.setCor(Color.Orange);
                        getVertice(atual).setCor(Color.Yellow);
                        somaPesos += a.getPeso(); // <- Soma o peso da aresta usada
                        Thread.Sleep(300);
                    }
                    atual = pai;
                }

                getVertice(origem).setCor(Color.Green);
                getVertice(destino).setCor(Color.Red);

                // Mostra o total do caminho mínimo
                MessageBox.Show("Caminho mínimo encontrado com peso total: " + somaPesos);
            }
            else
            {
                MessageBox.Show("Não há caminho entre os vértices selecionados.");
            }
        }
        public void pegarorigem()
        {
            int origem;

            MessageBox.Show("Clique no vértice de ORIGEM.");

            // Aguardar o clique do usuário
            while (getVerticeMarcado() == null)
            {
                Application.DoEvents(); // Permite que a interface do usuário continue a funcionar enquanto aguarda o clique
            }

            origem = getVerticeMarcado().getNum();
            getVertice(origem).setCor(Color.Pink);
            setVerticeMarcado(null);
            pegardesitno(origem);
        }
        public void pegardesitno(int origem)
        {

            int destino;

            MessageBox.Show("Clique no vértice de DESTINO.");

            // Aguardar o clique do usuário
            while (getVerticeMarcado() == null)
            {
                Application.DoEvents(); // Permite que a interface do usuário continue a funcionar enquanto aguarda o clique
            }

            destino = getVerticeMarcado().getNum();
            getVertice(destino).setCor(Color.Black);
            setVerticeMarcado(null);

            CaminhoMinimo(origem, destino);

        }
   
        public void CompletarGrafo()
        {
           for(int i = 0; i<getN(); i++)
            {
               
               for(int j = i+1; j< getN(); j++)
                {
                    setAresta(i, j, 1);
                }
            }
          
        }

        public bool IsEuleriano()
        {
            
            for (int i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                {
                    return false;

                }
               
            }
            return true;
        }

        public bool IsUnicursal()
        {
            int contador = 0;
           for(int i = 0; i<getN(); i++)
           {
            if(grau(i) % 2 != 0)
            {
                    contador++;
            }

           }
           if(contador == 2)
            {
                return true;
            }

            return false;
        }

        public void Largura(int v)
        {
            if (visitado == null)
            {
                visitado = new bool[matAdj.GetLength(0)];
            }

            Fila f = new Fila(matAdj.GetLength(0));
            f.enfileirar(v);
            visitado[v] = true;

            getVertice(v).setCor(Color.Pink); // 👉 pinta o vértice inicial

            while (!f.vazia())
            {
                v = f.desenfileirar();

                for (int i = 0; i < matAdj.GetLength(0); i++)
                {
                    Aresta aresta = getAresta(v, i);
                    if (aresta != null && !visitado[i])
                    {
                        visitado[i] = true;
                        f.enfileirar(i);

                        getVertice(i).setCor(Color.Pink); // 👉 pinta o vértice descoberto
                        aresta.setCor(Color.Orange);      // 👉 pinta a aresta usada

                        Thread.Sleep(500); // efeito visual de animação
                    }
                }
            }

        }

        public void NumeroCromatico()
        {
            int n = getN();
            int[] corVertice = new int[n];
            for (int i = 0; i < n; i++)
                corVertice[i] = -1;

            corVertice[0] = 0;

            for (int v = 1; v < n; v++)
            {
                bool[] coresUsadas = new bool[n];

                for (int i = 0; i < n; i++)
                {
                    if ((getAresta(v, i) != null || getAresta(i, v) != null) && corVertice[i] != -1)
                    {
                        coresUsadas[corVertice[i]] = true;
                    }
                }

                int corDisponivel;
                for (corDisponivel = 0; corDisponivel < n; corDisponivel++)
                {
                    if (!coresUsadas[corDisponivel])
                        break;
                }

                corVertice[v] = corDisponivel;
            }
            Color PegaCor(int cor)
            {
                switch (cor)
                {
                    case 0: return Color.Red;
                    case 1: return Color.Green;
                    case 2: return Color.Blue;
                    case 3: return Color.Yellow;
                    case 4: return Color.Orange;
                    case 5: return Color.Purple;
                    case 6: return Color.Orchid;
                    default: return Color.Gray; 
                }
            }
            int cores = 0;
         
            for (int i = 0; i < n; i++)
            {
                getVertice(i).setCor(PegaCor(corVertice[i]));
                cores = cores + i;
            
            }
            int color = 0;
            bool[] corUsada = new bool[n]; // no máximo n cores possíveis

            for (int i = 0; i < n; i++)
            {
                corUsada[corVertice[i]] = true; // marca as cores realmente usadas
            }

            // conta quantas cores diferentes foram usadas
            for (int i = 0; i < n; i++)
            {
                if (corUsada[i])
                    color++;
            }
            MessageBox.Show($"Número de cores usadas: {color}");


        }
       


        public String ParesOrdenados()
        {
            // getAresta(vi, vj) == null => aresta não existe
            // getVertice(v) => object "Vertice"
            //getVertice(0).setCor(Color.Red);
            //getVertice(0).setRotulo("BH");
            //getAresta(0, 1).setPeso(5);
            string msg = "";
            for(int i = 0; i<= getN(); i++)
            {
                for(int j = 1 ; j<= getN(); j++)
                {

                    if(getAresta(i,j) != null)
                    {
                        msg += "(" + getVertice(i).getRotulo() + "," + getVertice(j).getRotulo() + ")";
                        msg += "(" + getVertice(j).getRotulo() + "," + getVertice(i).getRotulo() + ")";
                    }
                    

                }

            }
            return ("E= (" + msg + ")");
        }
        public void Profundidade(int v)
        {
            if (visitado == null)
            {
                visitado = new bool[getN()];
            }

            visitado[v] = true;
            getVertice(v).setCor(Color.Pink); // marca o vértice como visitado visualmente

            for (int i = 0; i < getN(); i++) // melhor usar getN() se for o número de vértices
            {
                Aresta aresta = getAresta(v, i);
                if (aresta != null && !visitado[i])
                {
                    aresta.setCor(Color.Orange); // marca a aresta que será percorrida
                    Thread.Sleep(500); // pausa para visualização

                    Profundidade(i); // chamada recursiva
                }
            }
        }
    }
}
