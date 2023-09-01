using ProjetoXadrez.camTab;
using ProjetoXadrez.camTab.Enums;

namespace ProjetoXadrez.camXad {
    class Cavalo : Peca {
        public Cavalo(Tabuleiro tab, Cor cor, prtdXadrez partida) : base(tab, cor, partida) {
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);

            return p == null || p.cor != cor ? true : false;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            pos.definirValor(posicao.linha - 1, posicao.coluna - 2);
            if(tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValor(posicao.linha - 2, posicao.coluna - 1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValor(posicao.linha - 2, posicao.coluna + 1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValor(posicao.linha - 1, posicao.coluna + 2);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValor(posicao.linha + 1, posicao.coluna + 2);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValor(posicao.linha + 2, posicao.coluna + 1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValor(posicao.linha + 2, posicao.coluna - 1);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirValor(posicao.linha + 1, posicao.coluna - 2);
            if (tab.posValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            return mat;
        }

        public override string ToString() {
            return "C";
        }
    }
}
