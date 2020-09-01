import { Resposta } from '../model/resposta';

export class Etapa {
  numEtapa: number;
  textoEtapa: string;
  numProxEtapa: number;
  repostas: Resposta[];
  tipoEtapa: string;
}
