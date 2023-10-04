export class Tarefa{
    id: string;
    descricao: string;
    data: any;
    status: boolean;

    constructor(id: string, descricao: string, data: any, status: boolean){
        this.id = id;
        this.descricao = descricao;
        this.data= data;
        this.status = status;
    }
}