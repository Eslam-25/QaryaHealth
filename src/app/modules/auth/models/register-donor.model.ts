import { BloodType } from "../../../config/enums/blood-types.enum";
import { Gender } from "../../../config/enums/gender.enum";
import { Role } from "../../../config/enums/role.enum";

export interface RegisterDonorModel{
    id: number;
    name: string;
    address: string;
    phoneNumber: string;
    email: string;
    password: string;
    birthDate: Date;
    lastDonationDate: Date;
    bloodType: BloodType;
    gender: Gender;
    role: Role;
    readyToDonor: boolean;
}