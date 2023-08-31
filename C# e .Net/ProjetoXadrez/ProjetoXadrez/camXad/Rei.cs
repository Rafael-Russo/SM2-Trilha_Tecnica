﻿using ProjetoXadrez.camTab;
using ProjetoXadrez.camTab.Enums;

namespace ProjetoXadrez.camXad {
    class Rei : Peca{
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) {
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);

            return p == null || p.cor != cor ? true : false;
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

            return mat;
        }

        public override string ToString() {
            return "R";
        }
    }
}
