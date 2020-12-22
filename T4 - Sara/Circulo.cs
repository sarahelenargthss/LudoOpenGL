/**
  Autor: Sara Helena Régis Theiss
**/

using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    enum Eixos
    {
        X_Y,
        X_Z,
        Y_Z
    }

    internal class Circulo : ObjetoGeometria
    {
        int qntPontos;
        double raio;

        public Circulo (char rotulo, Objeto paiRef, int qntPontos, double raio, Ponto4D centro, Eixos eixos) : base(rotulo, paiRef)
        {
            this.qntPontos = qntPontos;
            this.raio = raio;

            Ponto4D p = new Ponto4D();
            Ponto4D q = new Ponto4D();
            double ang = 360/qntPontos;
            int cont = 0;
            for (int i = 0; i < qntPontos; i++ )
            {
                if(cont == 1)
                {
                    q = base.PontosUltimo();
                    base.PontosAdicionar(new Ponto4D(centro.X, centro.Y, centro.Z));
                }

                p = Matematica.GerarPtosCirculo(ang*i, raio);
                switch(eixos)
                {
                    case Eixos.X_Y:
                        base.PontosAdicionar(new Ponto4D(p.X+centro.X, p.Y+centro.Y, centro.Z));
                        break;
                    case Eixos.X_Z:
                        base.PontosAdicionar(new Ponto4D(p.X+centro.X, centro.Y, p.Y+centro.Z));
                        break;
                    case Eixos.Y_Z:
                        base.PontosAdicionar(new Ponto4D(centro.X, p.X+centro.Y, p.Y+centro.Z));
                        break;
                }
                cont++;

                if(cont == 2)
                {
                    p = base.PontosUltimo();
                    base.PontosAdicionar(new Ponto4D(q.X, q.Y, q.Z));
                    base.PontosAdicionar(new Ponto4D(p.X, p.Y, p.Z));
                    cont = 0;
                }
            }
        }

        protected override void DesenharObjeto()
        {
            GL.Begin(base.PrimitivaTipo); 
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex3(pto.X, pto.Y, pto.Z);
            }
            GL.End();
        }

        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Círculo: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }

    }
}