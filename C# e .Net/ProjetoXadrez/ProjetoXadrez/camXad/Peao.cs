using ProjetoXadrez.camTab;
using ProjetoXadrez.camTab.Enums;

namespace ProjetoXadrez.camXad {
    class Peao : Peca {
        public Peao(Tabuleiro tab, Cor cor, prtdXadrez partida) : base(tab, cor, partida) {
        }

        private bool existeInimigo(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos) {
            return tab.peca(pos) == null;
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);

            return p == null || p.cor != cor ? true : false;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if(cor == Cor.BRANCA) {
                pos.definirValor(posicao.linha - 1, posicao.coluna);
                if(tab.posValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValor(posicao.linha - 2, posicao.coluna);
                if (tab.posValida(pos) && livre(pos) && qtdMovimento == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValor(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValor(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //EnPassant
                if(posicao.linha == 3) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if(tab.posValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }
            } else {
                pos.definirValor(posicao.linha + 1, posicao.coluna);
                if (tab.posValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValor(posicao.linha + 2, posicao.coluna);
                if (tab.posValida(pos) && livre(pos) && qtdMovimento == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValor(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValor(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //EnPassant
                if (posicao.linha == 4) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }
            return mat;
        }

        public override string ToString() {
            return "P";
        }
    }
}
