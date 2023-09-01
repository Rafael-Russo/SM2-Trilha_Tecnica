using ProjetoXadrez.camTab;
using ProjetoXadrez.camTab.Enums;

namespace ProjetoXadrez.camXad {
    class Rei : Peca{

        private prtdXadrez partida;

        public Rei(Tabuleiro tab, Cor cor, prtdXadrez partida) : base(tab, cor, partida) {
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);

            return p == null || p.cor != cor ? true : false;
        }

        private bool testeTorreRoque(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qtdMovimento == 0;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //cima
            pos.definirValor(posicao.linha - 1, posicao.coluna);
            if(tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //cima direita
            pos.definirValor(posicao.linha - 1, posicao.coluna+1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //direita
            pos.definirValor(posicao.linha, posicao.coluna+1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //baixo direita
            pos.definirValor(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //baixo
            pos.definirValor(posicao.linha + 1, posicao.coluna);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //baixo esquerda
            pos.definirValor(posicao.linha + 1, posicao.coluna-1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //esquerda
            pos.definirValor(posicao.linha, posicao.coluna-1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //cima esquerda
            pos.definirValor(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            if(qtdMovimento == 0 && !partida.isXeque) {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreRoque(posT1)) {
                    Posicao pos1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao pos2 = new Posicao(posicao.linha, posicao.coluna + 2);

                    if(tab.peca(pos1) == null && tab.peca(pos2) == null) {
                        mat[posicao.linha, pos.coluna + 2] = true;
                    }
                }

                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreRoque(posT2)) {
                    Posicao pos1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao pos2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao pos3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (tab.peca(pos1) == null && tab.peca(pos2) == null && tab.peca(pos3) == null) {
                        mat[posicao.linha, pos.coluna - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString() {
            return "R";
        }
    }
}
