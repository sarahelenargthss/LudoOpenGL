using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;

namespace gcgcg
{
    enum Face
    {
        CIMA,
        BAIXO,
        FRENTE,
        FUNDO,
        DIREITA,
        ESQUERDA
    }

    internal class Dado : ObjetoGeometria
    {
        private Random r;
        private int numero = 0; // o número atual sorteado para o dado (aparece em cima), começando por 0 para representar o 1
        private float tamanhoDado = 0;
        private int[] numeros = new int[6] {5,4,3,2,1,0};
        private Cor cor;
        private Cor cor1;
        private Cor cor2;

        public Dado(char rotulo, Objeto paiRef, Ponto4D pontoInicial, float tamanho, Cor a, Cor b) : base(rotulo, paiRef)
        {      
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y + tamanho, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y + tamanho, pontoInicial.Z));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y, pontoInicial.Z - tamanho));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y, pontoInicial.Z - tamanho));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X + tamanho, pontoInicial.Y + tamanho, pontoInicial.Z - tamanho));
            base.PontosAdicionar(new Ponto4D(pontoInicial.X, pontoInicial.Y + tamanho, pontoInicial.Z - tamanho));
            r = new Random();
            tamanhoDado = tamanho;
            cor1 = a;
            cor2 = b;

            cor = cor1;

            girarDado();
        }

        // desenha o número em uma face do dado
        private void desenhaNumeroDado(int numero, Face face)
        {
            // salva em quantas colunas os círculos aparecem para cada número (o número é indicado pelo índice)
            int[] intervaloA = new int[6] {2,4,6,4,6,4};
            int[] intervaloB = new int[6] {2,4,6,4,6,6};
            
            Eixos eixos = Eixos.X_Y;
            Ponto4D ptoCentro = null;
            Circulo c;
            double somarA = 0;
            double somarB = 0;
            for(int i = 0; i <= numero; i++)
            {   
                // adiciona o ponto central do círculo de acordo com a face
                switch(face)
                {
                    case Face.CIMA:
                        ptoCentro = new Ponto4D(base.pontosLista[0].X + tamanhoDado/intervaloA[numero] + somarA, base.pontosLista[2].Y+0.01, base.pontosLista[0].Z - tamanhoDado/intervaloB[numero] - somarB);
                        eixos = Eixos.X_Z;
                        break;
                    case Face.BAIXO:
                        ptoCentro = new Ponto4D(base.pontosLista[0].X + tamanhoDado/intervaloA[numero] + somarA, base.pontosLista[0].Y-0.01, base.pontosLista[0].Z - tamanhoDado/intervaloB[numero] - somarB);
                        eixos = Eixos.X_Z;
                        break;
                    case Face.FRENTE:
                        ptoCentro = new Ponto4D(base.pontosLista[0].X + tamanhoDado/intervaloA[numero] + somarA, base.pontosLista[0].Y + tamanhoDado/intervaloB[numero] + somarB, base.pontosLista[0].Z+0.01);
                        break;
                    case Face.FUNDO:
                        ptoCentro = new Ponto4D(base.pontosLista[0].X + tamanhoDado/intervaloA[numero] + somarA, base.pontosLista[0].Y + tamanhoDado/intervaloB[numero] + somarB, base.pontosLista[4].Z-0.01);
                        break;                    
                    case Face.DIREITA:
                        ptoCentro = new Ponto4D(base.pontosLista[1].X+0.01, base.pontosLista[1].Y + tamanhoDado/intervaloB[numero] + somarB, base.pontosLista[1].Z - tamanhoDado/intervaloA[numero] - somarA);
                        eixos = Eixos.Y_Z;
                        break;
                    case Face.ESQUERDA:
                        ptoCentro = new Ponto4D(base.pontosLista[0].X-0.01, base.pontosLista[4].Y + tamanhoDado/intervaloB[numero] + somarB, base.pontosLista[4].Z + tamanhoDado/intervaloA[numero] + somarA);
                        eixos = Eixos.Y_Z;
                        break;
                }
                
                // altera os valores que devem ser somados nas coordenadas do ponto central do próximo círculo
                if( ((numero == 2 || numero == 4) && (i == 0 || i == 1)) ||
                    ((numero == 1 || numero == 3 || numero == 5) && i == 0))

                {
                    somarA += 2 * (tamanhoDado/intervaloA[numero]);
                    somarB += 2 * (tamanhoDado/intervaloB[numero]);
                }
                else if( (numero == 4 && i == 2) ||
                    ((numero == 3 || numero == 5) && i == 1))
                {
                    somarA = 0;   
                }
                else if(numero == 4 && i == 3)
                {
                    somarA = 4 * (tamanhoDado/intervaloA[numero]);
                    somarB = 0;
                } 
                else if((numero == 3 || numero == 5) && i == 2)
                {
                    somarA = 2 * (tamanhoDado/intervaloA[numero]);
                    somarB = 0;
                }
                else if(numero == 5 && i == 3)
                {
                    somarA = 0;
                    somarB = 4 * (tamanhoDado/intervaloB[numero]);
                }
                else if(numero == 5 && i == 4)
                {
                    somarA = 2 * (tamanhoDado/intervaloA[numero]);
                }
                
                // adiciona o círculo
                c = new Circulo(Utilitario.charProximo(), this, 15, tamanhoDado/8, ptoCentro, eixos);
                c.ObjetoCor.CorR = 0; c.ObjetoCor.CorG = 0; c.ObjetoCor.CorB = 0;
                c.PrimitivaTipo = PrimitiveType.TriangleStrip;
            }
            
        }

        protected override void DesenharObjeto()
        {   
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
            
            GL.Color3(cor.CorR/255f, cor.CorG/255f, cor.CorB/255f);
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

            GL.Color3(0.9f,0.9f,0.9f);
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

        public void mudaCor(int jogador)
        {
            // muda a cor do jogador atual
            if(jogador == 1)
                cor = cor1;
            else
                cor = cor2;
        }

        public int girarDado()
        {
            // sorteia um novo número
            numero = r.Next(0, 6);
            base.FilhosRemoverTodos(); // remove todos os círculos do dado

            // adiciona os novos círculos com base no novo número
            int[] verifica = new int[6] {0,0,0,0,0,0};

            desenhaNumeroDado(numero, Face.CIMA); // o número sorteado sempre ficará em cima
            verifica[numero] = 1;

            desenhaNumeroDado(numeros[numero], Face.BAIXO); // o número oposto sempre ficará embaixo
            verifica[numeros[numero]] = 1;

            bool primeiraDupla = true;
            for(int i = 0; i < 6; i++)
            {
                if(verifica[i] == 0) // verifica os número que ainda não foram adicionados
                {
                    if(primeiraDupla) // os dois segundos números ficam na frente e atrás
                    {
                        desenhaNumeroDado(i, Face.FRENTE);
                        verifica[i] = 1;

                        desenhaNumeroDado(numeros[i], Face.FUNDO);
                        verifica[numeros[i]] = 1;

                        primeiraDupla = false;
                    }
                    else // os dois últimos números ficam na direita e esquerda
                    {
                        desenhaNumeroDado(i, Face.DIREITA);
                        verifica[i] = 1;

                        desenhaNumeroDado(numeros[i], Face.ESQUERDA);
                        verifica[numeros[i]] = 1;

                        break;
                    }
                    
                }
            }

            return numero + 1; // retorna o numero = 1, para representar o número real que deve ser usado no tabuleiro
        }

        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Cubo: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
            retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }

    }
}