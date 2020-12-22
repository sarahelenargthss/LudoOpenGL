/**
  Autor: João Victor Braun Quintino
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;

namespace gcgcg
{
  internal class SegmentoReta : ObjetoGeometria
  {
    private Ponto4D pOriginal1;
    private Ponto4D pOriginal2;

    public SegmentoReta(char rotulo, Objeto paiRef, Ponto4D a, Ponto4D b) : base(rotulo, paiRef)
    { 
      pOriginal1 = new Ponto4D(a);
      pOriginal2 = new Ponto4D(b);
      base.PontosAdicionar(a);
      base.PontosAdicionar(b);
    }

    public void RestaurarValoresIniciais()
    {
      base.PontosRemoverTodos();
      base.PontosAdicionar(pOriginal1);
      base.PontosAdicionar(pOriginal2);
    }

    public void SomarPonto(int posicao, double x, double y)
    {
      try
      {
          pontosLista[posicao].X += x;
          pontosLista[posicao].Y += y;
      }
      catch (System.IndexOutOfRangeException e)
      {
          System.ArgumentException argumentException = new System.ArgumentException("Posição inválida", "posição", e);
          throw argumentException;
      }
    }

    protected override void DesenharObjeto()
    {
      GL.Begin(base.PrimitivaTipo);
      foreach (Ponto4D pto in pontosLista)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End();
    }
    //TODO: melhorar para exibir não só a lsita de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Circulo: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }

  }
}