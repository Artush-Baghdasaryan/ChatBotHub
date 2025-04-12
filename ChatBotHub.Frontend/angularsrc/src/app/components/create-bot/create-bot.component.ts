import { Component, ElementRef, EventEmitter, inject, Output, ViewChild } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { ChatBotStateService } from 'src/app/services/chat-bot-state.service';
import { ChatBotService } from 'src/app/services/chat-bot.service';
import { FileUploadModel } from 'src/app/types/file.model';

@Component({
  selector: 'app-create-bot',
  templateUrl: './create-bot.component.html',
  styleUrls: ['./create-bot.component.scss']
})
export class CreateBotComponent {

  private readonly _snackBar = inject(MatSnackBar);
  private readonly _chatBotState = inject(ChatBotStateService);
  private readonly _router = inject(Router);

  @Output() public chatBotCreated = new EventEmitter<void>();
  
  @ViewChild("fileInput") private fileInput!: ElementRef<HTMLInputElement>;

  public chatBotForm: FormGroup;
  public isLoading: boolean = false;

  constructor() {
    this.chatBotForm = new FormGroup({
      name: new FormControl('', Validators.required),
      files: new FormArray([])
    });
  }

  public onFileSelect(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file && this.isValidFile(file)) {
      this.createFileControl(file);

      this._snackBar.open(`${file.name} added`, 'Close', {
        duration: 3000,
        horizontalPosition: 'right',
        verticalPosition: 'bottom'
      });
    }
  }

  private createFileControl(file: File): void {
    const formGroup = new FormGroup({
      file: new FormControl(file),
      description: new FormControl("", Validators.required)
    });

    const filesArray = this.chatBotForm.get("files") as FormArray;

    filesArray.push(formGroup);
  }

  public getFiles(): FileUploadModel[] {
    return this.chatBotForm.get('files')?.value as FileUploadModel[];
  }

  public getFilesControls() {
    const res = (this.chatBotForm.get("files") as FormArray).controls;
    console.log(res);

    return res;
  }

  public onFileRemove(file: FileUploadModel): void {
    const files = this.chatBotForm.get('files')?.value as File[];
    this.chatBotForm.patchValue({ files: files.filter(f => f !== file.file) });

    const dt = new DataTransfer();
    this.getFiles().forEach(f => dt.items.add(f.file));
    this.fileInput.nativeElement.files = dt.files;

    this._snackBar.open(`${file.file.name} removed`, 'Close', {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'bottom'
    });
  }

  public createChatBot(): void {
    this.isLoading = true;

    const name = this.chatBotForm.get('name')?.value;
    const files = this.getFiles();

    console.log("create bot, files", files);

    this._chatBotState.addChatBot(name, files)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      ).subscribe(botId => {
        this._router.navigate(['/chat-bot', botId]);
      });
  }

  private isValidFile(file: File): boolean {
    const allowedTypes = [
      'application/pdf',                                                         // PDF
      'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',      // XLSX
      'application/vnd.ms-excel',                                               // XLS
      'application/msword',                                                     // DOC
      'application/vnd.openxmlformats-officedocument.wordprocessingml.document' // DOCX
    ];

    return allowedTypes.includes(file.type);
  }

}
