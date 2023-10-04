import { Component } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Tarefa } from 'src/model/Tarefa.model';
import { HomeService } from './home.service';
import { HttpErrorResponse } from '@angular/common/http';

import { CreateTarefaCommand } from 'src/model/CreateTarefaCommand.model';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  formulario: FormGroup | any;
  minDate: Date = new Date();
  tarefa: Tarefa = new Tarefa("", "", this.formataData(this.minDate), false);
  tarefas: Tarefa[] = [];
  IsWait: boolean = false;
  check = false;
  semResultado = "";
  dateRangeMask = "d0/M0/0000 Hh:m0";
  modoEditar: boolean = false;

  constructor(private formBuilder: FormBuilder, private homeService: HomeService) { }


  ngOnInit(): void {
    this.formulario = this.formBuilder.group({
      id: [''],
      descricao: ['', Validators.required],
      data: ['', Validators.required],
      status: [false]
    });

    this.check = this.tarefa.status
    this.formulario.setValue({
      id: this.tarefa.id,
      descricao: this.tarefa.descricao,
      data: this.tarefa.data,
      status: this.check
    })

    this.buscaTarefas('');
  }

  buscaTarefas(param: string): void {
    this.cleanForm();
    this.IsWait = true;
    this.tarefas = [];
    this.homeService.buscaTarefas(param)
      .subscribe({
        next: (res) => {
          this.IsWait = false;
          this.tarefas = res;
        },
        error: (err: HttpErrorResponse) => {
          // alert(`Erro ao buscar tarefas ${err.error.data}`);
          this.IsWait = false;
        }
      });
  }

  cancelarEdicao() {
    this.cleanForm();
  }

  cleanForm() {
    this.check = false
    this.formulario.setValue({
      id: '',
      descricao: '',
      data: this.formataData(this.minDate),
      status: this.check
    });
    this.modoEditar = false;
  }

  changeCheckStatus() {
    this.check = !this.check;
    this.tarefa.status = this.check;
    this.formulario.controls.status.value = this.check;
  }

  onClick(tarefa: any) {

  }

  criarTarefa() {
    var novaTarefa = new CreateTarefaCommand(this.formulario.controls.descricao.value, generateData(this.formulario.controls.data.value));

    this.IsWait = true;
    this.homeService.criaTarefa(novaTarefa)
      .subscribe({
        next: (res) => {
          if (res.success)
          {
            this.IsWait = false;
            alert("Tarefa enviada para a Fila com sucesso. Aguarde processamento!");
          }
        },
        error: (err: HttpErrorResponse) => {
          alert(`Erro ao criar tarefas ${err.error.data[0].message}`);
          this.IsWait = false;
        }
      });
  }

  salvar() {
    this.IsWait = true;
    var tarefa = new Tarefa(this.formulario.controls.id.value,
      this.formulario.controls.descricao.value,
      generateData(this.formulario.controls.data.value),
      this.formulario.controls.status.value);

    this.homeService.atualizaTarefa(tarefa)
      .subscribe({
        next: (res) => {
          if (res.success)
            alert("Atualizado com sucesso")
            
            this.buscaTarefas('');
        },
        error: (err: HttpErrorResponse) => {
          alert(`Erro ao atualizar tarefas ${err.error.data?.erro}`);
          this.IsWait = false;
        }
      });
  }

  editarTarefa(tarefaPar: Tarefa) {
    var data = new Date(tarefaPar.data);
    this.check = tarefaPar.status
    this.formulario.setValue({
      id: tarefaPar.id,
      descricao: tarefaPar.descricao,
      data: this.formataData(data),
      status: this.check
    })
    this.modoEditar = true;

  }

  deletarTarefa(tarefaPar: Tarefa){
    this.homeService.deletarTarefa(tarefaPar.id)
    .subscribe({
      next: (res) => {
        if (res)
          alert("Tarefa removida com sucesso")
          this.buscaTarefas('');
      },
      error: (err: HttpErrorResponse) => {
        alert(`Erro ao deletar tarefas ${err.error?.data[0]?.erro}`);
        this.IsWait = false;
      }
    });
    
  }

  formataData(dataStart: Date): string {
    const dataConsulta =
      adicionaZero(dataStart.getDate()).toString() +
      adicionaZero(dataStart.getMonth() + 1).toString() +
      dataStart.getFullYear();

    const horaIni = adicionaZero(dataStart.getHours())
    const minIni = adicionaZero(dataStart.getMinutes());

    const dataFinal = `${dataConsulta}${horaIni}${minIni}`;
    return dataFinal;
  }

}

function adicionaZero(numero: number) {
  if (numero <= 9) return '0' + numero;
  else return numero;
}

function generateData(value: any) {
  const dia = value.substr(0, 2);
  const mes = value.substr(2, 2);
  const ano = value.substr(4, 4);
  const hora = value.substr(8, 2);
  const min = value.substr(10, 2);
  const data = new Date(ano as number, mes as number -1, dia as number, hora as number, min as number);
  return data
}