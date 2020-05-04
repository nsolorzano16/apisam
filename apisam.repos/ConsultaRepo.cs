﻿using System;
using System.Linq;
using apisam.entities;
using apisam.entities.ViewModels;
using apisam.interfaces;
using apisam.repositories;
using ServiceStack.OrmLite;

namespace apisam.repos
{
    public class ConsultaRepo : IConsulta
    {
        private readonly OrmLiteConnectionFactory dbFactory;
        private readonly Conexion con = new Conexion();
        private static TimeZoneInfo hondurasTime;

        public ConsultaRepo()
        {
            var _connString = con.GetConnectionString();
            dbFactory = new OrmLiteConnectionFactory(_connString, SqlServerDialect.Provider);
            hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        }

        public ConsultaViewModel GetDetalleConsulta(int doctorId, int pacienteId, int preclinicaId)
        {
            ConsultaViewModel _resp = new ConsultaViewModel();
            using var _db = dbFactory.Open();
            var _preclinica = _db.Single<Preclinica>(x => x.PreclinicaId ==
            preclinicaId && x.PacienteId == pacienteId && x.DoctorId == doctorId
            && x.Activo == true && x.Atendida == true);

            var _antecedentesPersonales = _db.Single<AntecedentesFamiliaresPersonales>
                (x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _habitos = _db.Single<Habitos>(x => x.PacienteId
            == pacienteId && x.DoctorId == doctorId && x.Activo == true);

            var _historialGinecoObstetra = _db.Single<HistorialGinecoObstetra>(
                x => x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _farmacos = _db.Select<FarmacosUsoActual>
                (x => x.PacienteId ==
                pacienteId && x.DoctorId == doctorId && x.Activo == true).ToList();

            var _examenFisico = _db.Single<ExamenFisico>(
                x => x.PreclinicaId == preclinicaId && x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _examenFisicoGinecologico = _db.Single<ExamenFisicoGinecologico>(
                x => x.PreclinicaId == preclinicaId && x.PacienteId == pacienteId
                && x.DoctorId == doctorId && x.Activo == true);

            var _diagnosticos = _db.Select<Diagnosticos>(x => x.PreclinicaId == preclinicaId
            && x.PacienteId == pacienteId
            && x.DoctorId == doctorId && x.Activo == true).ToList();

            var _notas = _db.Select<Notas>(x => x.PreclinicaId == preclinicaId
            && x.PacienteId == pacienteId && x.DoctorId == doctorId && x.Activo == true).ToList();

            _resp.Preclinica = _preclinica;
            _resp.AntecedentesFamiliaresPersonales = _antecedentesPersonales;
            _resp.Habitos = _habitos;
            _resp.HistorialGinecoObstetra = _historialGinecoObstetra;
            _resp.FarmacosUsoActual = _farmacos;
            _resp.ExamenFisico = _examenFisico;
            _resp.ExamenFisicoGinecologico = _examenFisicoGinecologico;
            _resp.Diagnosticos = _diagnosticos;
            _resp.Notas = _notas;

            return _resp;
        }



        public RespuestaMetodos AddConsultaGeneral(ConsultaGeneral consulta)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                consulta.CreadoFecha = dateTime_HN;
                consulta.ModificadoFecha = dateTime_HN;
                _db.Save<ConsultaGeneral>(consulta);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }

            return _resp;
        }

        public RespuestaMetodos UpdateConsultaGeneral(ConsultaGeneral consulta)
        {
            var _resp = new RespuestaMetodos();
            DateTime dateTime_HN = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            try
            {
                using var _db = dbFactory.Open();
                consulta.ModificadoFecha = dateTime_HN;
                _db.Save<ConsultaGeneral>(consulta);
                _resp.Ok = true;
            }
            catch (Exception ex)
            {
                _resp.Ok = false;
                _resp.Mensaje = ex.Message;
            }
            return _resp;
        }

        public ConsultaGeneral GetConsultaGeneralById(int consultaId)
        {
            using var _db = dbFactory.Open();
            var antecedente = _db.Single<ConsultaGeneral>(x =>
            x.ConsultaId == consultaId && x.Activo == true);
            return antecedente;
        }


    }
}