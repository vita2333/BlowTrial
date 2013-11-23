using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;
using BlowTrial.Domain.Tables;
using BlowTrial.Models;

namespace BlowTrial.Infrastructure.Automapper
{
    public class ScreenProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ScreenedPatient, NewPatientModel>()
                .ForMember(d=>d.DateOfBirth, opt=>opt.Ignore())
                .ForMember(d=>d.TimeOfBirth, opt=>opt.Ignore())
                .ForMember(d=>d.GestAgeDays, opt=>opt.Ignore())
                .ForMember(d=>d.GestAgeWeeks, opt=>opt.Ignore());
        }
    }

    public class PatientProfiles : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Participant, ParticipantModel>()
                .ForMember(d=>d.DeathOrLastContactDate, o=>o.Ignore())
                .ForMember(d => d.DeathOrLastContactTime, o => o.Ignore())
                .ForMember(d => d.DischargeDate, o => o.Ignore())
                .ForMember(d => d.DischargeTime, o => o.Ignore())
                .ForMember(d=>d.VaccinesAdministered, o=>o.Ignore())
                .ForMember(d=>d.VaccineModelsAdministered, o=>o.MapFrom(s=>s.VaccinesAdministered))
                .ForMember(d=>d.StudyCentre, o=>o.Ignore())
                .AfterMap((s,d) => {
                    foreach (var v in d.VaccineModelsAdministered)
                    {
                        v.AdministeredTo = d;
                    }
                });

            Mapper.CreateMap<Participant, ParticipantCsvModel>()
                .ForMember(d=>d.CauseOfDeathId, o=>o.MapFrom(s=>(int)s.CauseOfDeath))
                .ForMember(d => d.OutcomeAt28Id, o => o.MapFrom(s => (int)s.OutcomeAt28Days));

            Mapper.CreateMap<ScreenedPatient, ScreenedPatientCsvModel>();

            Mapper.CreateMap<ProtocolViolation, ProtocolViolationModel>();
        }
    }

    public class VaccineAdministeredProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<VaccineAdministered, VaccineAdministeredModel>();
                //.ForMember(s=>s.VaccineGiven, o=>o.MapFrom(d=>d.VaccineGiven));
        }
    }
}