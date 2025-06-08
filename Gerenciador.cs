using System;
using System.Collections.Generic;
using System.IO;
public class Gerenciador
{
    public List<Tarefa> tarefas = new List<Tarefa>();
    public void Menu()
    {
        Console.WriteLine("Bem-vindo ao Gerenciador de Tarefas!");
        Console.WriteLine("O que deseja fazer?");
        Console.WriteLine("1 - Adicionar");
        Console.WriteLine("2 - Listar");
        Console.WriteLine("3 - Editar");
        Console.WriteLine("4 - Deletar");
        Console.WriteLine("5 - Salvar em .txt");
        Console.WriteLine("6 - Carregar de .txt");
        Console.WriteLine("7 - Sair");
        string opcao = Console.ReadLine()??"";
        switch (opcao)
        {
            case "1":
                Adicionar();  // Chama o método Adicionar;
                Console.WriteLine("Tarefa adicionada com sucesso!\n");
                Menu(); // Chama o método Menu novamente para continuar o fluxo;
                break;
            case "2":
                Listar(); // Chama o método Listar;
                Console.WriteLine("Menus l // Chama o método Menu novamente para continuar o fluxo;istadas com sucesso!\n");
                Menu();
                break;
            case "3":
                Editar(); // Chama o método Editar;
                Console.WriteLine("Tarefa editada com sucesso!\n");
                Menu();
                break;
            case "4":
                Remover(); // Chama o método Remover;
                Console.WriteLine("Tarefa removida com sucesso!\n");
                Menu();
                break;
            case "5":
                SalvaremTxt(); // Chama o método SalvaremTxt;
                Console.WriteLine("Tarefas salvas com sucesso!\n");
                Menu();
                break;
            case "6":
                CarregarDeTxt(); // Chama o método CarregarDeTxt;
                Console.WriteLine("Tarefas carregadas com sucesso!\n");
                Menu();
                break;
            case "7":
                Console.WriteLine("Saindo...\n");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opção inválida, tente novamente.\n");
                Menu();
                break;
        }
    }

    public void Adicionar()
    {
        Console.WriteLine("Digite o nome da tarefa:");
        string nome = Console.ReadLine()??"";
        Console.WriteLine("Digite a descrição da tarefa:");
        string descricao = Console.ReadLine()??"";
        Tarefa novaTarefa = new Tarefa
        {
            Nome = nome,
            Descricao = descricao,
            DataCriacao = DateTime.Now,
            Concluida = false
        };
        tarefas.Add(novaTarefa);
        Console.WriteLine($"Tarefa '{novaTarefa}' adicionada com sucesso!");
    }
    public void Listar()
    {
        if (tarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa cadastrada.");
        }
        else
        {
            Console.WriteLine("As seguintes tarefas foram cadastradas:");
            for (int i = 0; i < tarefas.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {tarefas[i]}");
            }
        }
    }

    public void Editar()
    {
        Console.WriteLine("Digite o número da tarefa que deseja editar:");
        if (tarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa cadastrada.");
            return;
        }
        else
        {
            if (int.TryParse(Console.ReadLine(), out int numeroTarefa))
            {
                if (numeroTarefa > 0 && numeroTarefa <= tarefas.Count)
                {
                    string novoNome;
                    do
                    {
                        Console.WriteLine("Digite o novo nome da tarefa:");
                        novoNome = Console.ReadLine() ?? ""; // Lê o novo nome da tarefa, garantindo que não seja nulo
                        if (string.IsNullOrWhiteSpace(novoNome))
                        {
                            Console.WriteLine("Não foi declarado nenhum nome de tarefa. Por favor, digite um nome válido"); // Solicita novamente o nome se estiver vazio ou apenas espaços em branco            }
                        }
                    } while (string.IsNullOrWhiteSpace(novoNome)); // Continua solicitando até que um nome válido seja fornecido


                    Console.WriteLine("Digite a nova descrição da tarefa:");
                    string novaDescricao = Console.ReadLine() ?? "";
                    tarefas[numeroTarefa - 1].Nome = novoNome;
                    tarefas[numeroTarefa - 1].Descricao = novaDescricao;
                    tarefas[numeroTarefa - 1].DataCriacao = DateTime.Now; // Atualiza a data de criação
                    tarefas[numeroTarefa - 1].Concluida = false; // Reseta o status de conclusão
                    Console.WriteLine($"Tarefa {numeroTarefa} editada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Número de tarefa inválido.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
            }
        }
    }
    public void Remover()
    {
        Console.WriteLine("Digite o número da tarefa que deseja remover:");
        if (tarefas.Count == 0)
        {
                Console.WriteLine("Nenhuma tarefa cadastrada.");
                return;
        }
        else
        {
        if (int.TryParse(Console.ReadLine(), out int numeroTarefa))
            {
                if (numeroTarefa > 0 && numeroTarefa <= tarefas.Count)
                {
                    string tarefaRemovida = tarefas[numeroTarefa - 1].ToString();
                    tarefas.RemoveAt(numeroTarefa - 1);
                    Console.WriteLine($"Tarefa '{tarefaRemovida}' removida com sucesso!");
                }
                else
                {
                Console.WriteLine("Número de tarefa inválido.");
                }
            }
        else
        {
            Console.WriteLine("Entrada inválida. Por favor, insira um número válido.");
        }
        }
    }
    public void SalvaremTxt()
    {
        using (StreamWriter writer = new StreamWriter("tarefas.txt"))
        {
            foreach (var tarefa in tarefas)
            {
                writer.WriteLine(tarefa.ToString());
            }
        }
        Console.WriteLine("Tarefas salvas em tarefas.txt com sucesso!");
    }

    public void CarregarDeTxt()
    {
        if (File.Exists("tarefas.txt"))
        {
            tarefas.Clear(); // Limpa a lista antes de carregar
            using (StreamReader reader = new StreamReader("tarefas.txt"))
            {
                string linha;
                while ((linha = reader.ReadLine()??"") != null)
                {
                    string[] partes = linha.Split(" - ");
                    if (partes.Length == 4)
                    {
                        Tarefa tarefaCarregada = new Tarefa
                        {
                            Nome = partes[0],
                            Descricao = partes[1],
                            DataCriacao = DateTime.Parse(partes[2].Replace("Criada em: ", "")),
                            Concluida = bool.Parse(partes[3].Replace("Concluída: ", ""))
                        };
                        tarefas.Add(tarefaCarregada);
                    }
                }
            }
            Console.WriteLine("Tarefas carregadas de tarefas.txt com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo tarefas.txt não encontrado.");
        }
    }
}