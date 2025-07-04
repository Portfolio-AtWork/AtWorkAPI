﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AtWork.Domain.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250611010618_recriarModel")]
    partial class recriarModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Funcionario", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<Guid?>("ID_Grupo")
                        .HasColumnType("uuid")
                        .HasColumnName("id_grupo");

                    b.Property<Guid>("ID_Usuario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("login");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("nome");

                    b.Property<string>("ST_Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("st_status");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("senha");

                    b.HasKey("ID");

                    b.HasIndex("ID_Grupo");

                    b.HasIndex("ID_Usuario");

                    b.ToTable("tb_funcionario");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Grupo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("nome");

                    b.Property<string>("ST_Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("st_status");

                    b.HasKey("ID");

                    b.ToTable("tb_grupo");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Grupo_X_Admin", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<Guid>("ID_Grupo")
                        .HasColumnType("uuid")
                        .HasColumnName("id_grupo");

                    b.Property<Guid>("ID_Usuario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.HasKey("ID");

                    b.HasIndex("ID_Grupo");

                    b.HasIndex("ID_Usuario");

                    b.ToTable("tb_grupo_x_admin");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Horario", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<Guid>("ID_Usuario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_usuario");

                    b.Property<string>("ST_Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("st_status");

                    b.HasKey("ID");

                    b.HasIndex("ID_Usuario");

                    b.ToTable("tb_horario");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Horario_X_Dia", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<string>("Dia_Da_Semana")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("dia_da_semana");

                    b.Property<TimeOnly>("Hora_Final")
                        .HasColumnType("time without time zone")
                        .HasColumnName("hora_final");

                    b.Property<TimeOnly>("Hora_Inicio")
                        .HasColumnType("time without time zone")
                        .HasColumnName("hora_inicio");

                    b.Property<Guid>("ID_Horario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_horario");

                    b.Property<string>("ST_Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("st_status");

                    b.HasKey("ID");

                    b.HasIndex("ID_Horario");

                    b.ToTable("tb_horario_x_dia");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Justificativa", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<DateTime>("DT_Justificativa")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_justificativa");

                    b.Property<Guid>("ID_Funcionario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_funcionario");

                    b.Property<string>("ImagemContentType")
                        .HasColumnType("text")
                        .HasColumnName("imagem_content_type");

                    b.Property<byte[]>("ImagemJustificativa")
                        .HasColumnType("bytea")
                        .HasColumnName("imagem_justificativa");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("justificativa");

                    b.Property<string>("ST_Justificativa")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("st_status");

                    b.HasKey("ID");

                    b.HasIndex("ID_Funcionario");

                    b.ToTable("tb_justificativa");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Ponto", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<DateTime>("DT_Ponto")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_ponto");

                    b.Property<Guid>("ID_Funcionario")
                        .HasColumnType("uuid")
                        .HasColumnName("id_funcionario");

                    b.Property<string>("ST_Ponto")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("st_ponto");

                    b.Property<string>("TP_Ponto")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("tp_ponto");

                    b.HasKey("ID");

                    b.HasIndex("ID_Funcionario");

                    b.ToTable("tb_ponto");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Usuario", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DT_Alt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_alt");

                    b.Property<DateTime>("DT_Cad")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("dt_cad");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("login");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("nome");

                    b.Property<string>("ST_Status")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("st_status");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("senha");

                    b.HasKey("ID");

                    b.ToTable("tb_usuario");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Funcionario", b =>
                {
                    b.HasOne("AtWork.Domain.Database.Entities.TB_Grupo", "GrupoFK")
                        .WithMany()
                        .HasForeignKey("ID_Grupo");

                    b.HasOne("AtWork.Domain.Database.Entities.TB_Usuario", "UsuarioFK")
                        .WithMany()
                        .HasForeignKey("ID_Usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GrupoFK");

                    b.Navigation("UsuarioFK");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Grupo_X_Admin", b =>
                {
                    b.HasOne("AtWork.Domain.Database.Entities.TB_Grupo", "GrupoFK")
                        .WithMany()
                        .HasForeignKey("ID_Grupo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AtWork.Domain.Database.Entities.TB_Usuario", "UsuarioFK")
                        .WithMany()
                        .HasForeignKey("ID_Usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GrupoFK");

                    b.Navigation("UsuarioFK");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Horario", b =>
                {
                    b.HasOne("AtWork.Domain.Database.Entities.TB_Usuario", "UsuarioFK")
                        .WithMany()
                        .HasForeignKey("ID_Usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UsuarioFK");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Horario_X_Dia", b =>
                {
                    b.HasOne("AtWork.Domain.Database.Entities.TB_Horario", "HorarioFK")
                        .WithMany()
                        .HasForeignKey("ID_Horario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HorarioFK");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Justificativa", b =>
                {
                    b.HasOne("AtWork.Domain.Database.Entities.TB_Funcionario", "FuncionarioFK")
                        .WithMany()
                        .HasForeignKey("ID_Funcionario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuncionarioFK");
                });

            modelBuilder.Entity("AtWork.Domain.Database.Entities.TB_Ponto", b =>
                {
                    b.HasOne("AtWork.Domain.Database.Entities.TB_Funcionario", "FuncionarioFK")
                        .WithMany()
                        .HasForeignKey("ID_Funcionario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuncionarioFK");
                });
#pragma warning restore 612, 618
        }
    }
}
