export class CreateTarefaCommand{
        descricao: string;
        data: Date;

        constructor(descricao: string, data: any){
            this.descricao = descricao;
            this.data= data;
        }
}