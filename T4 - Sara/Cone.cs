/**
  Autor: Dalton Solano dos Reis
**/

using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal class Cone : ObjetoGeometria
  {
    //TODO: gerar os vetores normais, tem como fazer no link deste exemplo
    private bool exibeVetorNormal = false;
    //TODO: não precisava ter parte negativa, ter um tipo inteiro grande
    protected List<int> listaTopologia = new List<int>();

    public Cone(char rotulo, Objeto paiRef) : base(rotulo, paiRef)
    {
      int segments = 10; // Números mais altos melhoram a qualidade 
      int radius = 1;    // O raio (largura) do cilindro
      int height = 2;   // A altura do cilindro

      for (double y = 0; y < 2; y++)
      {
        for (double x = 0; x < segments; x++)
        {
          double theta = (x / (segments - 1)) * 2 * Math.PI;
          base.PontosAdicionar(new Ponto4D(
              (float)(radius * Math.Cos(theta)),
              (float)(height * y),
              (float)(radius * Math.Sin(theta))));
        }
      }
      // ponto do centro da base
      base.PontosAdicionar(new Ponto4D(0,0,0));
      // ponto do centro da topo
      base.PontosAdicionar(new Ponto4D(0,height,0));

      //TODO: parce que alguams faces estão com a orientação errada.
      for (int x = 0; x < segments - 1; x++)
      {
        // base
        listaTopologia.Add(x);
        listaTopologia.Add(x + 1);
        listaTopologia.Add(segments - 1);
        // topo
        listaTopologia.Add(x);
        listaTopologia.Add(x + 1);
        listaTopologia.Add(segments);
      }

    }

    protected override void DesenharObjeto()
    {
      GL.Begin(PrimitiveType.Triangles);
      foreach (int index in listaTopologia)
        GL.Vertex3(base.pontosLista[index].X, base.pontosLista[index].Y, base.pontosLista[index].Z);
      GL.End();
    }

    //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Cone: " + base.rotulo + "\n";
      for (var i = 0; i < pontosLista.Count; i++)
      {
        retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
      }
      return (retorno);
    }

  }
}