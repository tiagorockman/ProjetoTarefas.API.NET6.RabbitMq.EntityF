import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Tarefa } from 'src/model/Tarefa.model';
import { GenericCommandResult } from 'src/model/GenericCommandResult.model';
import { CreateTarefaCommand } from 'src/model/CreateTarefaCommand.model';


@Injectable({
  providedIn: 'root'
})
export class HomeService {

  

  constructor(private http: HttpClient) { }

  public buscaTarefas(rota: string){
    const URL = environment.API + `/tarefas/${rota}`;

    return this.http.get<Tarefa[]>(URL);
  }

  public atualizaTarefa(tarefa: Tarefa) {
    
    const URL = environment.API + "/tarefas";
    return this.http.put<GenericCommandResult>(URL, tarefa);
  }

  public criaTarefa(novaTarefa: CreateTarefaCommand) {
    const URL = environment.API + "/tarefas";

    return this.http.post<GenericCommandResult>(URL, novaTarefa);
  }

  deletarTarefa(id: any) {
    const URL = environment.API + `/tarefas/${id}`;

    return this.http.delete<boolean>(URL);
  }

}
