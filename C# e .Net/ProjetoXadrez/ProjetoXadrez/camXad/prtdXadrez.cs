using ProjetoXadrez.camTab;
using ProjetoXadrez.camTab.Enums;

using System.Collections.Generic;

namespace ProjetoXadrez.camXad {
    class prtdXadrez {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public prtdXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.BRANCA;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementaMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validaOrigem(Posicao pos) {
            if(tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A peça na posição de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem selecionada!");
            }
        }

        public void validaDestino(Posicao origem, Posicao destino) {
            if(!tab.peca(origem).podeMovPara(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void mudaJogador() {
            if(jogadorAtual == Cor.BRANCA) {
                jogadorAtual = Cor.PRETA;
            } else {
                jogadorAtual = Cor.BRANCA;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas) {
                if(x.cor == cor) {
                    aux.Add(x);
                }
            }

            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));

            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeca('a', 8, new Torre(tab, Cor.PRETA));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.PRETA));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.PRETA));
            colocarNovaPeca('d', 8, new Rainha(tab, Cor.PRETA));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.PRETA));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.PRETA));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.PRETA));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.PRETA));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.PRETA));   
            colocarNovaPeca('b', 7, new Peao(tab, Cor.PRETA));   
            colocarNovaPeca('c', 7, new Peao(tab, Cor.PRETA));   
            colocarNovaPeca('d', 7, new Peao(tab, Cor.PRETA));   
            colocarNovaPeca('e', 7, new Peao(tab, Cor.PRETA));   
            colocarNovaPeca('f', 7, new Peao(tab, Cor.PRETA));   
            colocarNovaPeca('g', 7, new Peao(tab, Cor.PRETA));   
            colocarNovaPeca('h', 7, new Peao(tab, Cor.PRETA));

            colocarNovaPeca('a', 1, new Torre(tab, Cor.BRANCA));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.BRANCA));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.BRANCA));
            colocarNovaPeca('d', 1, new Rainha(tab, Cor.BRANCA));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.BRANCA));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.BRANCA));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.BRANCA));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.BRANCA));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.BRANCA));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.BRANCA));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.BRANCA));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.BRANCA));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.BRANCA));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.BRANCA));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.BRANCA));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.BRANCA));
        }
    }
}
