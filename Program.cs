using System;
using System.Collections.Generic;

public class Paciente
{
    public string Nome { get; set; }
    public double PressaoArterial { get; set; }
    public double Temperatura { get; set; }
    public double Oxigenacao { get; set; }
    public string Prioridade { get; set; }

    public Paciente(string nome, double pressaoArterial, double temperatura, double oxigenacao)
    {
        Nome = nome;
        PressaoArterial = pressaoArterial;
        Temperatura = temperatura;
        Oxigenacao = oxigenacao;
        Prioridade = ClassificarPrioridade();
    }

    public string ClassificarPrioridade()
    {
        if (PressaoArterial > 18 || Temperatura > 39 || Oxigenacao < 90)
            return "Vermelha";  // Prioridade Máxima
        else if (PressaoArterial > 15 || Temperatura > 37.5)
            return "Amarela";  // Prioridade Média
        else
            return "Verde";    // Prioridade Normal
    }
}

public class Triagem
{
    public Queue<Paciente> FilaTriagem { get; set; } = new Queue<Paciente>();

    public void AdicionarPaciente(Paciente paciente)
    {
        FilaTriagem.Enqueue(paciente);
    }
}

public class AtendimentoClinico
{
    public List<Paciente> FilaAtendimento { get; set; } = new List<Paciente>();

    public void AdicionarPacienteNaFila(Paciente paciente)
    {
        if (paciente.Prioridade == "Vermelha")
            FilaAtendimento.Insert(0, paciente); // Prioridade máxima vai para o começo
        else if (paciente.Prioridade == "Amarela")
            FilaAtendimento.Insert(1, paciente); // Prioridade média vai depois
        else
            FilaAtendimento.Add(paciente);  // Prioridade normal vai pro final
    }

    public void AtenderPaciente()
    {
        if (FilaAtendimento.Count > 0)
        {
            var paciente = FilaAtendimento[0];
            Console.WriteLine($"Atendendo paciente: {paciente.Nome} - Prioridade: {paciente.Prioridade}");
            FilaAtendimento.RemoveAt(0);  // Remove o paciente atendido
        }
        else
        {
            Console.WriteLine("Não há pacientes na fila.");
        }
    }
}

public class HistoricoAtendimentos
{
    public Stack<Paciente> Historico { get; set; } = new Stack<Paciente>();

    public void AdicionarAoHistorico(Paciente paciente)
    {
        Historico.Push(paciente);
    }

    public void MostrarHistorico()
    {
        Console.WriteLine("Histórico de atendimentos:");
        foreach (var paciente in Historico)
        {
            Console.WriteLine(paciente.Nome);
        }
    }
}
public class Program
{
    public static void Main()
    {
        Triagem triagem = new Triagem();
        AtendimentoClinico atendimento = new AtendimentoClinico();
        HistoricoAtendimentos historico = new HistoricoAtendimentos();

        // Adicionando pacientes
        Paciente p1 = new Paciente("João", 19, 38.5, 92);
        Paciente p2 = new Paciente("Maria", 15, 37, 95);
        Paciente p3 = new Paciente("Carlos", 20, 40, 85);

        triagem.AdicionarPaciente(p1);
        triagem.AdicionarPaciente(p2);
        triagem.AdicionarPaciente(p3);

        // Movendo os pacientes para a fila de atendimento
        while (triagem.FilaTriagem.Count > 0)
        {
            Paciente paciente = triagem.FilaTriagem.Dequeue();
            atendimento.AdicionarPacienteNaFila(paciente);
        }

        // Simulando o atendimento
        atendimento.AtenderPaciente();
        atendimento.AtenderPaciente();

        // Adicionando ao histórico
        historico.AdicionarAoHistorico(p1);
        historico.AdicionarAoHistorico(p2);

        // Mostrando o histórico
        historico.MostrarHistorico();
    }
}
