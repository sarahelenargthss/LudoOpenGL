using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;

namespace gcgcg
{
    internal class Peca : ObjetoGeometria
    {
        private Cor cor;
        private float tamanhoPeca = 0;
        public float TamanhoPeca { get => tamanhoPeca; }
        private Ambiente ambiente;
        public Ambiente AmbientePeca { get => ambiente; set => ambiente = value; }
        private int posicao;
        public int Posicao { get => posicao; set => posicao = value; }
        private int jogador;
        public int Jogador { get => jogador; }
        private int indicePeca;
        public int IndicePeca { get => indicePeca; }

        public Peca(char rotulo, Objeto paiRef, Ponto4D pontoInicial, float tamanho, int jogador, Cor cor, Ambiente ambiente, int posicao = 0) : base(rotulo, paiRef)
        {
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y + tamanho, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y + tamanho, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y, pontoInicial.Z - tamanho));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y, pontoInicial.Z - tamanho));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y + tamanho, pontoInicial.Z - tamanho));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y + tamanho, pontoInicial.Z - tamanho));
            this.tamanhoPeca = tamanho;
            this.cor = cor;
            this.ambiente = ambiente;
            this.posicao = posicao;
            this.jogador = jogador;
            this.indicePeca = posicao;
        }

        protected override void DesenharObjeto()
        {
            float r = cor.CorR - 70f > 0f ? cor.CorR - 70f : 0f;
            float g = cor.CorG - 70f > 0f ? cor.CorG - 70f : 0f;
            float b = cor.CorB - 70f > 0f ? cor.CorB - 70f : 0f;
            GL.Color3(r / 255f, g / 255f, b / 255f);
            // Sentido anti-horário
            GL.Begin(PrimitiveType.Quads);
            // Face da frente
            GL.Normal3(0, 0, 1);
            GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
            GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
            GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
            // Face do fundo
            GL.Normal3(0, 0, -1);
            GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF

            GL.Color3(cor.CorR / 255f, cor.CorG / 255f, cor.CorB / 255f);
            // Face de cima
            GL.Normal3(0, 1, 0);
            GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
            GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            // Face de baixo
            GL.Normal3(0, -1, 0);
            GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
            GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB

            GL.Color3(r / 255f, g / 255f, b / 255f);
            // Face da direita
            GL.Normal3(1, 0, 0);
            GL.Vertex3(base.pontosLista[1].X, base.pontosLista[1].Y, base.pontosLista[1].Z);    // PtoB
            GL.Vertex3(base.pontosLista[5].X, base.pontosLista[5].Y, base.pontosLista[5].Z);    // PtoF
            GL.Vertex3(base.pontosLista[6].X, base.pontosLista[6].Y, base.pontosLista[6].Z);    // PtoG
            GL.Vertex3(base.pontosLista[2].X, base.pontosLista[2].Y, base.pontosLista[2].Z);    // PtoC
            // Face da esquerda
            GL.Normal3(-1, 0, 0);
            GL.Vertex3(base.pontosLista[0].X, base.pontosLista[0].Y, base.pontosLista[0].Z);    // PtoA
            GL.Vertex3(base.pontosLista[3].X, base.pontosLista[3].Y, base.pontosLista[3].Z);    // PtoD
            GL.Vertex3(base.pontosLista[7].X, base.pontosLista[7].Y, base.pontosLista[7].Z);    // PtoH
            GL.Vertex3(base.pontosLista[4].X, base.pontosLista[4].Y, base.pontosLista[4].Z);    // PtoE
            GL.End();
        }

        public void movePeca(Ponto4D novoPonto)
        {
            int cont = 0;
            base.PontosAlterar(new Ponto4D(novoPonto.X, novoPonto.Y, novoPonto.Z), cont++);
            base.PontosAlterar(new Ponto4D(novoPonto.X + tamanhoPeca, novoPonto.Y, novoPonto.Z), cont++);
            base.PontosAlterar(new Ponto4D(novoPonto.X + tamanhoPeca, novoPonto.Y + tamanhoPeca, novoPonto.Z), cont++);
            base.PontosAlterar(new Ponto4D(novoPonto.X, novoPonto.Y + tamanhoPeca, novoPonto.Z), cont++);
            base.PontosAlterar(new Ponto4D(novoPonto.X, novoPonto.Y, novoPonto.Z - tamanhoPeca), cont++);
            base.PontosAlterar(new Ponto4D(novoPonto.X + tamanhoPeca, novoPonto.Y, novoPonto.Z - tamanhoPeca), cont++);
            base.PontosAlterar(new Ponto4D(novoPonto.X + tamanhoPeca, novoPonto.Y + tamanhoPeca, novoPonto.Z - tamanhoPeca), cont++);
            base.PontosAlterar(new Ponto4D(novoPonto.X, novoPonto.Y + tamanhoPeca, novoPonto.Z - tamanhoPeca), cont++);
        }

        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Peca: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }

    }
}