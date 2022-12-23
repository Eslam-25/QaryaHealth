import { BloodType } from "../enums/blood-types.enum";

export interface BloodTypeModel {
    type: BloodType;
    name: string;
}

export function BloodTypes(): BloodTypeModel[] {
    return [
        { type: BloodType.APlus, name: "+A" },
        { type: BloodType.AMins, name: "-A" },
        { type: BloodType.BPlus, name: "+B" },
        { type: BloodType.BMins, name: "-B" },
        { type: BloodType.ABPlus, name: "+AB" },
        { type: BloodType.ABMins, name: "-AB" },
        { type: BloodType.OPlus, name: "+O" },
        { type: BloodType.OMins, name: "-O" },
    ];
}