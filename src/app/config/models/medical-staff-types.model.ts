import { MedicalStaffType } from "../enums/medical-staff-types.enum";

export interface MedicalStaffTypeModel {
    type: MedicalStaffType;
    name: string;
    imagePath: string;
}

export function LabTypes(): MedicalStaffTypeModel[] {
    return [
        { type: MedicalStaffType.AnaylsisLab, name: "معامل تحاليل", imagePath: "../../../assets/img/anaylsis-lab.png" },
        { type: MedicalStaffType.XRayLab, name: "معامل اشعة", imagePath: "../../../assets/img/xray.png" },
    ];
}

export function NurseTypes(): MedicalStaffTypeModel[] {
    return [
        { type: MedicalStaffType.Nurse, name: "تمريض عام", imagePath: "../../../assets/img/nurse.png" },
    ];
}

export function MedicalTypes(): MedicalStaffTypeModel[] {
    return [
        { type: MedicalStaffType.Pediatricians, name: "اطفال", imagePath: "../../../assets/img/anaylsis-lab1.png" },
        { type: MedicalStaffType.Dentists, name: "اسنان", imagePath: "../../../assets/img/anaylsis-lab1.png" },
        { type: MedicalStaffType.EarAndNose, name: "انف واذن", imagePath: "../../../assets/img/anaylsis-lab1.png" },
        { type: MedicalStaffType.Abdomen, name: "باطنة", imagePath: "../../../assets/img/anaylsis-lab1.png" },
    ];
}

export function getMedicalStaffByType(type: MedicalStaffType): MedicalStaffTypeModel{
    let staff = LabTypes().find(r => r.type == type);
    
    if(staff == null)
        staff = NurseTypes().find(r => r.type == type);
    if(staff == null)
        staff = MedicalTypes().find(r => r.type == type);

    return staff;
}