public class Tarefa
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public bool Concluida { get; set; }
    public override string ToString()
    {
        return $"{Nome} - {Descricao} (Criada em: {DataCriacao}, Concluída: {Concluida})";
    }
}