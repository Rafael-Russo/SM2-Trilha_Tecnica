﻿using ProjetoXadrez.camTab;

namespace ProjetoXadrez.camXad {
    class PosXadrez {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosXadrez(char coluna, int linha) {
            this.coluna = coluna;
            this.linha = linha;
        }

        public Posicao toPosicao() {
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString() {
            return "" + coluna + linha;
        }
    }
}
