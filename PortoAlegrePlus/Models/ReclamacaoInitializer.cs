using System;
using System.Collections.Generic;
using System.Linq;

namespace PortoAlegrePlus.Models
{

    public class ReclamacaoInitializer : System.Data.Entity.DropCreateDatabaseAlways<PortoAlegrePlusDBContext>
    {
        protected override void Seed(PortoAlegrePlusDBContext context)
        {
            var categorias = new List<Categoria>
                {
                new Categoria { Nome = "Acessibilidade", Descricao = "Calçada bloqueada, acesso para deficientes físicos, carro público em vaga especial [...]" },
                new Categoria { Nome = "Árvores", Descricao = "Corte irregular de árvore, queda de árvore, poda de árvore, árvore atrapalhando fios de energia [...]" },
                new Categoria { Nome = "Atendimento / SAC", Descricao = "Críticas ao atendimento da prefeitura, falta de informação [...]" },
                new Categoria { Nome = "Buracos", Descricao = "Buracos em via pública." },
                new Categoria { Nome = "Cultura, Esporte e Lazer", Descricao = "Praças, parques, atividades de lazer, shows & eventos" },
                new Categoria { Nome = "Educação", Descricao = "Escolas Municipais e creches." },
                new Categoria { Nome = "Iluminação Pública", Descricao = "Todo tipo de reclamação relacionada a iluminação pública." },
                new Categoria { Nome = "Lixo & Poluição", Descricao = "Lixo acumulado, poluição do AR, ausência de coleta de lixo domiciliar [...]" },
                new Categoria { Nome = "Meio Ambiente", Descricao = "Rios, desmatamento, recolhimento de animal silvestre, resíduos sólidos [...]" },
                new Categoria { Nome = "Obras Públicas", Descricao = "Bueiros, esgoto, pontes, passarelas, prédios públicos [...]" },
                new Categoria { Nome = "Poluição Sonora", Descricao = "Barulho de obra, barulho de vizinho, barulho de bares e similares." },
                new Categoria { Nome = "Saúde", Descricao = " Postos de Saúde, hospitais, ambulatórios e zoonoses." },
                new Categoria { Nome = "Segurança / Denúcia", Descricao = "Convívio público e segurança em geral." },
                new Categoria { Nome = "Trânsito", Descricao = "Multa de trânsito, congestionamento, semáforo com defeito, acidente de carro [...]" },
                new Categoria { Nome = "Transporte Público", Descricao = "Táxi, Ônibus e Trensurb." }

                };

            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();

            var bairros = new List<Bairro>
                {
                new Bairro { Nome = "Bom Jesus" },
                new Bairro { Nome = "Camaquã" },
                new Bairro { Nome = "Cascata" },
                new Bairro { Nome = "Centro" },
                new Bairro { Nome = "Coronel Aparício Borges" },
                new Bairro { Nome = "Cristal" },
                new Bairro { Nome = "Jardim Carvalho" },
                new Bairro { Nome = "Jardim Itu-Sabará" },
                new Bairro { Nome = "Lomba do Pinheiro" },
                new Bairro { Nome = "Mário Quintana" },
                new Bairro { Nome = "Menino Deus" },
                new Bairro { Nome = "Nonoai" },
                new Bairro { Nome = "Partenon" },
                new Bairro { Nome = "Passo D'Areia" },
                new Bairro { Nome = "Petrópolis" },
                new Bairro { Nome = "Restinga" },
                new Bairro { Nome = "Rubem Berta" },
                new Bairro { Nome = "Santa Tereza" },
                new Bairro { Nome = "Santana" },
                new Bairro { Nome = "São José" },
                new Bairro { Nome = "Sarandi" },
                new Bairro { Nome = "Vila Ipiranga" },
                new Bairro { Nome = "Vila Nova" },
                new Bairro { Nome = "Zona Indefinida" }
                };

            bairros.ForEach(s => context.Bairros.Add(s));
            context.SaveChanges();

            var reclamacoes = new List<Reclamacao>
                {

                new Reclamacao {
                Titulo = "Casa noturna com som alto",
                CategoriaID =  categorias.Single( c => c.Nome == "Poluição Sonora").CategoriaID,
                Descricao = "Toda a noite é a mesma coisa, já estou cansado",
                DataReclamacao = DateTime.Parse("2016-5-11"),
                BairroID = bairros.Single( b => b.Nome == "Centro").BairroID,
                Endereco = "R. Beirute, 45",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Motorista de ônibus mal educado",
                CategoriaID =  categorias.Single( c => c.Nome == "Transporte Público").CategoriaID,
                Descricao = "Falta de educação com os passageiros. Linha 210, horário das 11h35, Bairo-Centro",
                DataReclamacao = DateTime.Parse("2016-5-28"),
                BairroID = bairros.Single( b => b.Nome == "Restinga").BairroID,
                Endereco = "Estrada João Antônio da Silveira, 3330",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Lixo acumulando na Av. Rocha Loires",
                CategoriaID =  categorias.Single( c => c.Nome == "Lixo & Poluição").CategoriaID,
                Descricao = "Faz uns 3 dias que o caminhão do lixo não passa",
                DataReclamacao = DateTime.Parse("2016-6-3"),
                BairroID = bairros.Single( b => b.Nome == "Nonoai").BairroID,
                Endereco = "Av. Rocha Loires, 442",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Assalto à luz do dia",
                CategoriaID =  categorias.Single( c => c.Nome == "Segurança / Denúcia").CategoriaID,
                Descricao = "Segundo assalto no mesmo mês. Cadê a Brigada Militar?",
                DataReclamacao = DateTime.Parse("2016-5-10"),
                BairroID = bairros.Single( b => b.Nome == "Santa Tereza").BairroID,
                Endereco = "Rua Silvério, 128",
                Status = "Aberta"

                },

                new Reclamacao {
                Titulo = "Árvore caída no meio da calçada",
                CategoriaID =  categorias.Single( c => c.Nome == "Árvores").CategoriaID,
                Descricao = "Há uma árvore caída do lado da parada 4, os ônibus mal conseguem passar",
                DataReclamacao = DateTime.Parse("2016-6-3"),
                BairroID = bairros.Single( b => b.Nome == "Lomba do Pinheiro").BairroID,
                Endereco = "Parada 4",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Bueiro entupido",
                CategoriaID =  categorias.Single( c => c.Nome == "Obras Públicas").CategoriaID,
                Descricao = "Bueiro entupido, além do mal cheiro, os ratos estão fazendo a festa no local",
                DataReclamacao = DateTime.Parse("2016-5-31"),
                BairroID = bairros.Single( b => b.Nome == "Vila Ipiranga").BairroID,
                Endereco = "Rua Professor José Maria Rodrigues",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Lâmpada apagada à noite",
                CategoriaID =  categorias.Single( c => c.Nome == "Iluminação Pública").CategoriaID,
                Descricao = "Lâmpada do poste apagada.",
                DataReclamacao = DateTime.Parse("2016-6-6"),
                BairroID = bairros.Single( b => b.Nome == "Jardim Carvalho").BairroID,
                Endereco = "Rua João Mendes Ouríques, 650",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Poda/retirada de árvore",
                CategoriaID =  categorias.Single( c => c.Nome == "Árvores").CategoriaID,
                Descricao = "Está árvore está enrolada nos fios.",
                DataReclamacao = DateTime.Parse("2016-6-1"),
                BairroID = bairros.Single( b => b.Nome == "Restinga").BairroID,
                Endereco = "Avenida Vereador Milton Pozzolo de Oliveira",
                StatusOficial = true
                },

                new Reclamacao {
                Titulo = "Serviço mal feito",
                CategoriaID =  categorias.Single( c => c.Nome == "Cultura, Esporte e Lazer").CategoriaID,
                Descricao = "Não cortam a grama que está invadindo a calçada/passeio. Serviço mal feito.",
                DataReclamacao = DateTime.Parse("2016-5-29"),
                BairroID = bairros.Single( b => b.Nome == "Bom Jesus").BairroID,
                Endereco = "Rua Doutor Alfredo Matias Wiltgen, 515",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Muito lixo na rua",
                CategoriaID =  categorias.Single( c => c.Nome == "Lixo & Poluição").CategoriaID,
                Descricao = "Lixo na frente da Igreja da Auxiliadora",
                DataReclamacao = DateTime.Parse("2016-6-5"),
                BairroID = bairros.Single( b => b.Nome == "Partenon").BairroID,
                Endereco = "Rua Auxiliadora, 268",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Veículo abandonado há muito tempo",
                CategoriaID =  categorias.Single( c => c.Nome == "Segurança / Denúcia").CategoriaID,
                Descricao = "Este carro está ali há mais de um ano, com pneus murchos.",
                DataReclamacao = DateTime.Parse("2016-6-4"),
                BairroID = bairros.Single( b => b.Nome == "Lomba do Pinheiro").BairroID,
                Endereco = "R. Anchieta, 333",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Buraco muito grande",
                CategoriaID =  categorias.Single( c => c.Nome == "Buracos").CategoriaID,
                Descricao = "Cratera no lado esquerdo da via, próximo ao meio fio.",
                DataReclamacao = DateTime.Parse("2016-6-2"),
                BairroID = bairros.Single( b => b.Nome == "Passo D'Areia").BairroID,
                Endereco = "R. Miguel Tostes, 464",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Equipamento público danificado",
                CategoriaID =  categorias.Single( c => c.Nome == "Obras Públicas").CategoriaID,
                Descricao = "Tampa do bueiro quebrada por caminhão.",
                DataReclamacao = DateTime.Parse("2016-5-30"),
                BairroID = bairros.Single( b => b.Nome == "Vila Nova").BairroID,
                Endereco = "Av. Luiz Moschetti, 90",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Vazamento de água",
                CategoriaID =  categorias.Single( c => c.Nome == "Obras Públicas").CategoriaID,
                Descricao = "Tubulação rompida!",
                DataReclamacao = DateTime.Parse("2016-5-27"),
                BairroID = bairros.Single( b => b.Nome == "Petrópolis").BairroID,
                Endereco = "Av. Plínio Brasil Milano, 242",
                Status = "Encerrada",
                StatusOficial = true
                },

                new Reclamacao {
                Titulo = "Mercedes Prata estacionada na calçada",
                CategoriaID =  categorias.Single( c => c.Nome == "Acessibilidade").CategoriaID,
                Descricao = "Uma vergonha. Cadê a EPTC?",
                DataReclamacao = DateTime.Parse("2016-6-5"),
                BairroID = bairros.Single( b => b.Nome == "Mário Quintana").BairroID,
                Endereco = "R. Santo Inácio, 159-205",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Esgoto a céu aberto",
                CategoriaID =  categorias.Single( c => c.Nome == "Obras Públicas").CategoriaID,
                Descricao = "Perto do colégio Anchieta",
                DataReclamacao = DateTime.Parse("2016-6-6"),
                BairroID = bairros.Single( b => b.Nome == "Petrópolis").BairroID,
                Endereco = "Av. Nilo Peçanha, 1432-1442",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Fiação irregular da prefeitura",
                CategoriaID =  categorias.Single( c => c.Nome == "Segurança / Denúcia").CategoriaID,
                Descricao = "Perigo à vista.",
                DataReclamacao = DateTime.Parse("2016-5-30"),
                BairroID = bairros.Single( b => b.Nome == "Centro").BairroID,
                Endereco = "Avenida Borges de Medeiros, 1176-1188",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Inferno das Raves",
                CategoriaID =  categorias.Single( c => c.Nome == "Poluição Sonora").CategoriaID,
                Descricao = "Moradores localizados a mais de 600m do local não conseguiram dormir.",
                DataReclamacao = DateTime.Parse("2016-5-26"),
                BairroID = bairros.Single( b => b.Nome == "Santa Tereza").BairroID,
                Endereco = "Av. Principal da Ponta Grossa, 4926-5108",
                Status = "Aberta"
                },

                new Reclamacao {
                Titulo = "Placa de sinalização quebrada",
                CategoriaID =  categorias.Single( c => c.Nome == "Obras Públicas").CategoriaID,
                Descricao = "Os moradores tiveram que colocar uma placa sinalizando a rua.",
                DataReclamacao = DateTime.Parse("2016-6-4"),
                BairroID = bairros.Single( b => b.Nome == "Cascata").BairroID,
                Endereco = "R. Barão de Ubá, 805",
                Status = "Aberta",
                StatusUsuario = true
                }


                };

            reclamacoes.ForEach(s => context.Reclamacoes.Add(s));
            context.SaveChanges();
        }
    }
}