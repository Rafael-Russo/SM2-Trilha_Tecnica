using ProjetoXadrez.camTab;
using ProjetoXadrez.camTab.Enums;

namespace ProjetoXadrez.camXad {
    class Bispo : Peca {
        public Bispo(Tabuleiro tab, Cor cor, prtdXadrez partida) : base(tab, cor, partida) {
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);

            return p == null || p.cor != cor ? true : false;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //cima direita
            pos.definirValor(posicao.linha - 1, posicao.coluna -1);
            while (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.definirValor(pos.linha - 1, pos.coluna - 1);
            }

            //cima esquerda
            pos.definirValor(posicao.linha - 1, posicao.coluna + 1);
            while (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.definirValor(pos.linha - 1, pos.coluna + 1);
            }

            //baixo direita
            pos.definirValor(posicao.linha + 1, posicao.coluna + 1);
            while (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.definirValor(pos.linha + 1, pos.coluna + 1);
            }

            //baixo esquerda
            pos.definirValor(posicao.linha + 1, posicao.coluna - 1);
            while (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.definirValor(pos.linha + 1, pos.coluna - 1);
            }
            return mat;
        }

        public override string ToString() {
            return "B";
        }
    }
}
