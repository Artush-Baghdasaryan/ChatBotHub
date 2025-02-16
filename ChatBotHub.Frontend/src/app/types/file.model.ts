export interface FileModel {
    id: string;
    fileName: string;
    fileType: string;
    uploadDate: Date;
}

export interface FileUploadModel {
    file: File;
    description: string;
}