import { FileModel } from "./file.model";

export interface ChatBotModel {
    id: string;
    name: string;
    files: FileModel[];
}