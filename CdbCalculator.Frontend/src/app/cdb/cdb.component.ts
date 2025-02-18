import { Component } from "@angular/core";
import { CdbService } from "../services/cdb.service";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { NgxMaskDirective, provideNgxMask } from "ngx-mask";

@Component({
  selector: "app-cdb",
  templateUrl: "./cdb.component.html",
  styleUrls: ["./cdb.component.scss"],
  standalone: true,
  imports: [FormsModule, CommonModule, NgxMaskDirective],
  providers: [provideNgxMask()],
})
export class CdbComponent {
  initialValue: number = 0;
  months: number = 0;
  grossYield: number | null = null;
  netYield: number | null = null;
  errorMessage: string | null = null;

  constructor(private cdbService: CdbService) {}

  onInitialValueChange(value: any): void {
    if (value !== undefined && value !== null) {
      this.initialValue = value;
    }

    this.clearErrorMessage();
  }

  onMonthsChange(value: any): void {
    this.clearErrorMessage();
  }

  onSubmit(): void {

    if(!this.validateValues()) return;

    this.cdbService.calculateCdb(this.initialValue, this.months).subscribe(
      (response) => {
        this.grossYield = response.grossYield;
        this.netYield = response.netYield;
      },
      (error) => {
        this.errorMessage = "Erro ao calcular o rendimento. Tente novamente!";
        console.error(error);
      }
    );
  }

  clearErrorMessage() {
    this.errorMessage = "";
  }

  validateValues() {
    if (this.initialValue <= 0) {
      this.errorMessage = "Por favor, informe um valor válido.";
      return false;
    }
    if (this.months <= 1) {
      this.errorMessage = "Por favor, informe um prazo maior que 1.";
      return false;
    }
    if (this.initialValue >= 10000000000000.0) {
      this.errorMessage = "Por favor, informe um valor menor que R$ 10.000.000.000.000,00";
      return false;
    }
    if (this.months >= 1000) {
      this.errorMessage = "Por favor, informe um prazo menor que 1000 meses.";
      return false;
    }

    return true;
  }
}
