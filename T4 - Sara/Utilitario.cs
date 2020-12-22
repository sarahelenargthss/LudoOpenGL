/**
  Autor: Dalton Solano dos Reis
**/

using System;

namespace gcgcg
{
  public abstract class Utilitario
  {
    public static char id = '@';

    public static char charProximo() {
      id = Convert.ToChar(id + 1);
      return id;
    }

    public static void AjudaTeclado()
    {
      Console.WriteLine(" --- Ajuda / Teclas: ");
      Console.WriteLine(" [  H   ] mostra está ajuda. ");
      Console.WriteLine(" [  F1  ] mostra ajuda do jogo. ");
      Console.WriteLine(" [Escape] sair. ");
      Console.WriteLine(" [Enter ] joga o dado para o jogador da vez. ");
      Console.WriteLine(" [  X   ] selecionam o eixo x. ");
      Console.WriteLine(" [  Y   ] selecionam o eixo y. ");
      Console.WriteLine(" [  Z   ] selecionam o eixo z. ");
      Console.WriteLine(" [  B   ] desenha BBox do objeto selecionado (inicia com o tabuleiro selecionado). ");
      Console.WriteLine(" [  E   ] lista objetos. ");
      Console.WriteLine(" [Minus ] diminui o deslocamento. ");
      Console.WriteLine(" [ Plus ] aumenta o deslocamento. ");
      Console.WriteLine(" [  C   ] ativa opções para câmera. ");
      Console.WriteLine(" [  O   ] ativa opções para objeto. ");
      Console.WriteLine(" [  P   ] mostra características de câmera/objeto (aquele que estiver selecionado). ");
      Console.WriteLine(" [  M   ] lista matriz do objeto. ");
      Console.WriteLine(" [  R   ] restaura valores de câmera/objeto (aquele que estiver selecionado)");
      Console.WriteLine(" [Right ] aplica deslocamento positivo ");
      Console.WriteLine(" [ Left ] aplica deslocamento negativo. ");
      Console.WriteLine(" [  Up  ] muda opção de câmera/objeto (aquele que estiver selecionado). ");
      Console.WriteLine(" [ Down ] muda opção de câmera/objeto (aquele que estiver selecionado). ");
    }

    public static void AjudaJogo()
    {
      Console.WriteLine("");
      Console.WriteLine(" --- FUNCIONAMENTO DO JOGO ");
      Console.WriteLine(" Para iniciar o jogo, o jogador com o lado azul joga o dado pressionando a tecla [Enter]. ");
      Console.WriteLine(" Para identificar de qual jogador é a vez, a câmera estará posicionada de modo que a casa ");
      Console.WriteLine(" do mesmo apareça na frente e o dado tenha a face de cima com a cor correspondente.");
      Console.WriteLine(" -");
      Console.WriteLine(" Exemplo: na vez do vermelho, o dado ficará vermelho e a casinha vermelha estará mais próxima do jogador.");
      Console.WriteLine(" -");
      Console.WriteLine(" Para tirar as peças das casinhas, é necessário o número 6. Sempre que o dado obter o número 6");
      Console.WriteLine(" e tiver uma peça ainda na casa, ela é retirada. Caso não hajam mais peças na casa, as peças comuns do tabuleiro");
      Console.WriteLine(" são movidas. Ao pegar o número 6 no dado, o jogador pode jogar novamente em qualquer momento do jogo.");
      Console.WriteLine(" -");
      Console.WriteLine(" Quando uma peça entra dentro do caminho final (marcado em vermelho e azul para cada jogador) é necessário obter o");
      Console.WriteLine(" número exato para chegar na última casa. Caso não seja obtido, outra peça do jogador é movida, se tiver uma peça");
      Console.WriteLine(" dele no tabuleiro.");
      Console.WriteLine(" -");
      Console.WriteLine(" Ganha o jogador que conseguir salvar suas 4 peças primeiro. As peças salvas ficam nas plataformas do lado contrário");
      Console.WriteLine(" da casa e com a mesma cor que ela.");
      Console.WriteLine("");
    }

  }
}