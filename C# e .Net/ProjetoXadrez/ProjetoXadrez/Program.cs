using ProjetoXadrez.camTab;
using ProjetoXadrez.camXad;

namespace ProjetoXadrez {
    class Program {
        public static void Main(string[] args) {

            try {
                prtdXadrez partida = new prtdXadrez();

                while (!partida.terminada) {
                    try {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Digite a origem: ");
                        Posicao origem = Tela.lerPosXadrez().toPosicao();
                        partida.validaOrigem(origem);

                        bool[,] posPosiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.tab, posPosiveis);

                        Console.WriteLine();
                        Console.Write("Digite o destino: ");
                        Posicao destino = Tela.lerPosXadrez().toPosicao();
                        partida.validaDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }catch(TabuleiroException ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
                Console.Clear();
                Tela.imprimirPartida(partida);

            } catch (TabuleiroException ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}