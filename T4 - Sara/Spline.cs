/**
  Autor: Sara Helena Régis Theiss
**/

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System;
using System.Collections.Generic;

namespace gcgcg
{
  internal class Spline : ObjetoGeometria
  {
    private List<Ponto4D> pontosSpline = new List<Ponto4D>();
    public bool bMostrarPontos = true;
    public Cor PontosCor = new Cor(0,0,0,255);
    public int PontosTamanho = 1;
    public Cor PontoSelecionadoCor = new Cor(0,0,0,255);
    public int PontoSelecionado = 0;
    public int qtdPontos = 0;
    private int qtdPontosOriginal = 0;
    private Ponto4D pOriginal1;
    private Ponto4D pOriginal2;
    private Ponto4D pOriginal3;
    private Ponto4D pOriginal4;

    public Spline(char rotulo, Objeto paiRef, int quantidadePontos, Ponto4D a, Ponto4D b, Ponto4D c, Ponto4D d) : base(rotulo, paiRef)
    { 
      qtdPontosOriginal = quantidadePontos;
      qtdPontos = quantidadePontos;

      pOriginal1 = new Ponto4D(a);
      pOriginal2 = new Ponto4D(b);
      pOriginal3 = new Ponto4D(c);
      pOriginal4 = new Ponto4D(d);
      base.PontosAdicionar(a);
      base.PontosAdicionar(b);
      base.PontosAdicionar(c);
      base.PontosAdicionar(d);
    }

    private void CarregarPontosSpline()
    {
      pontosSpline.Clear();
      Ponto4D pto;
      double pesoP0;
      double pesoP1;
      double pesoP2;
      double pesoP3;
      double inc = 1.0 / qtdPontos;
      for (double t = 0; t <= 1; t += inc)
      {
          pesoP0 = Math.Pow(1 - t, 3);
          pesoP1 = 3 * t * Math.Pow(1 - t, 2);
          pesoP2 = 3 * Math.Pow(t, 2) * (1 - t);
          pesoP3 = Math.Pow(t, 3);

          pto = new Ponto4D();
          pto.X = pesoP0 * pontosLista[0].X + pesoP1 * pontosLista[1].X + pesoP2 * pontosLista[2].X + pesoP3 * pontosLista[3].X;
          pto.Y = pesoP0 * pontosLista[0].Y + pesoP1 * pontosLista[1].Y + pesoP2 * pontosLista[2].Y + pesoP3 * pontosLista[3].Y;
          pontosSpline.Add(pto);
      }
    }

    public void RestaurarValoresIniciais()
    {
      base.PontosRemoverTodos();
      base.PontosAdicionar(pOriginal1);
      base.PontosAdicionar(pOriginal2);
      base.PontosAdicionar(pOriginal3);
      base.PontosAdicionar(pOriginal4);
      qtdPontos = qtdPontosOriginal;
      PontoSelecionado = 0;
    }

    public void SomarPontoControle(double x, double y)
    {
      try
      {
          pontosLista[PontoSelecionado-1].X += x;
          pontosLista[PontoSelecionado-1].Y += y;
      }
      catch (System.IndexOutOfRangeException e)
      {
          System.ArgumentException argumentException = new System.ArgumentException("Posição inválida", "posição", e);
          throw argumentException;
      }
    }

    protected override void DesenharObjeto()
    {      
      CarregarPontosSpline();

      GL.Begin(base.PrimitivaTipo);
      foreach (Ponto4D pto in pontosSpline)
      {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End();

      if(bMostrarPontos)
      {
        int cont = 0;
        GL.PointSize(PontosTamanho);
        GL.Begin(PrimitiveType.Points);
        GL.Color3(PontosCor.CorR, PontosCor.CorG, PontosCor.CorB);
        foreach (Ponto4D pto in pontosLista)
        {
          cont++;
          if(PontoSelecionado > 0 && cont == PontoSelecionado)
          {
            GL.Color3(PontoSelecionadoCor.CorR, PontoSelecionadoCor.CorG, PontoSelecionadoCor.CorB);
            GL.Vertex2(pto.X, pto.Y);
            GL.Color3(PontosCor.CorR, PontosCor.CorG, PontosCor.CorB);
          }
          else
            GL.Vertex2(pto.X, pto.Y);
        }
        GL.End();
      }
    }

  }
}