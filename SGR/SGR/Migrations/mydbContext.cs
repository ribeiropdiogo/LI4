using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SGR.Migrations
{
    public partial class mydbContext : DbContext
    {
        public mydbContext()
        {
        }

        public mydbContext(DbContextOptions<mydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artigo> Artigo { get; set; }
        public virtual DbSet<ArtigoInPedido> ArtigoInPedido { get; set; }
        public virtual DbSet<DataHora> DataHora { get; set; }
        public virtual DbSet<Fornecedor> Fornecedor { get; set; }
        public virtual DbSet<Funcionario> Funcionario { get; set; }
        public virtual DbSet<Gerente> Gerente { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<Mercadoria> Mercadoria { get; set; }
        public virtual DbSet<MercadoriaInArtigo> MercadoriaInArtigo { get; set; }
        public virtual DbSet<Mesa> Mesa { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PrecoMercadoriaFornecedor> PrecoMercadoriaFornecedor { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;database=mydb;uid=root;sslmode=Preferred", x => x.ServerVersion("8.0.18-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artigo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Preco)
                    .HasColumnName("preco")
                    .HasColumnType("decimal(8,0)");
            });

            modelBuilder.Entity<ArtigoInPedido>(entity =>
            {
                entity.HasIndex(e => e.IdArtigo)
                    .HasName("fk2_pedidos_idx");

                entity.HasIndex(e => e.IdPedido)
                    .HasName("fk1_aip");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdArtigo)
                    .HasColumnName("idArtigo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdPedido)
                    .HasColumnName("idPedido")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdArtigoNavigation)
                    .WithMany(p => p.ArtigoInPedido)
                    .HasForeignKey(d => d.IdArtigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2_aip");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.ArtigoInPedido)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1_aip");
            });

            modelBuilder.Entity<DataHora>(entity =>
            {
                entity.HasIndex(e => e.IdHorario)
                    .HasName("fk_datahora_1");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataHora1)
                    .HasColumnName("dataHora")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdHorario)
                    .HasColumnName("idHorario")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdHorarioNavigation)
                    .WithMany(p => p.DataHora)
                    .HasForeignKey(d => d.IdHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_datahora_1");
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasIndex(e => e.IdGerente)
                    .HasName("gerente_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Contacto)
                    .IsRequired()
                    .HasColumnName("contacto")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdGerente)
                    .HasColumnName("idGerente")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdGerenteNavigation)
                    .WithMany(p => p.Fornecedor)
                    .HasForeignKey(d => d.IdGerente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1_f");
            });

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasIndex(e => e.IdGerente)
                    .HasName("gerente_idx");

                entity.HasIndex(e => e.IdHorario)
                    .HasName("idHorario_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cargo)
                    .IsRequired()
                    .HasColumnName("cargo")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DataNascimento)
                    .HasColumnName("dataNascimento")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdGerente)
                    .HasColumnName("idGerente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdHorario)
                    .HasColumnName("idHorario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Morada)
                    .IsRequired()
                    .HasColumnName("morada")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdGerenteNavigation)
                    .WithMany(p => p.Funcionario)
                    .HasForeignKey(d => d.IdGerente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2");

                entity.HasOne(d => d.IdHorarioNavigation)
                    .WithMany(p => p.Funcionario)
                    .HasForeignKey(d => d.IdHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1");
            });

            modelBuilder.Entity<Gerente>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Mercadoria>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Embalagem)
                    .IsRequired()
                    .HasColumnName("embalagem")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Marca)
                    .HasColumnName("marca")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasColumnType("varchar(32)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Observacoes)
                    .IsRequired()
                    .HasColumnName("observacoes")
                    .HasColumnType("varchar(128)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.QuantidadeMinima)
                    .HasColumnName("quantidadeMinima")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Stock)
                    .HasColumnName("stock")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<MercadoriaInArtigo>(entity =>
            {
                entity.HasIndex(e => e.IdArtigo)
                    .HasName("nomeArtigo_idx");

                entity.HasIndex(e => e.IdMercadoria)
                    .HasName("fk1_mia_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdArtigo)
                    .HasColumnName("idArtigo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdMercadoria)
                    .HasColumnName("idMercadoria")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdArtigoNavigation)
                    .WithMany(p => p.MercadoriaInArtigo)
                    .HasForeignKey(d => d.IdArtigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2_mia");

                entity.HasOne(d => d.IdMercadoriaNavigation)
                    .WithMany(p => p.MercadoriaInArtigo)
                    .HasForeignKey(d => d.IdMercadoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1_mia");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ocupacao)
                    .IsRequired()
                    .HasColumnName("ocupacao")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasIndex(e => e.IdFuncionario)
                    .HasName("fk1_pedido_idx");

                entity.HasIndex(e => e.Mesa)
                    .HasName("mesa_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataHora)
                    .HasColumnName("dataHora")
                    .HasColumnType("datetime");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdFuncionario)
                    .HasColumnName("idFuncionario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mesa)
                    .HasColumnName("mesa")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Numero)
                    .HasColumnName("numero")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1_pedido");

                entity.HasOne(d => d.MesaNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.Mesa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2_pedido");
            });

            modelBuilder.Entity<PrecoMercadoriaFornecedor>(entity =>
            {
                entity.HasIndex(e => e.Fornecedor)
                    .HasName("fornecedor_idx");

                entity.HasIndex(e => e.Mercadoria)
                    .HasName("mecadoria_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fornecedor)
                    .HasColumnName("fornecedor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mercadoria)
                    .HasColumnName("mercadoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Preco)
                    .HasColumnName("preco")
                    .HasColumnType("decimal(8,0)");

                entity.HasOne(d => d.FornecedorNavigation)
                    .WithMany(p => p.PrecoMercadoriaFornecedor)
                    .HasForeignKey(d => d.Fornecedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1_pmf");

                entity.HasOne(d => d.MercadoriaNavigation)
                    .WithMany(p => p.PrecoMercadoriaFornecedor)
                    .HasForeignKey(d => d.Mercadoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2_pmf");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasIndex(e => e.IdGerente)
                    .HasName("gerente_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataHora)
                    .HasColumnName("dataHora")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdGerente)
                    .HasColumnName("idGerente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NifCliente)
                    .IsRequired()
                    .HasColumnName("nifCliente")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasColumnName("nomeCliente")
                    .HasColumnType("varchar(64)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdGerenteNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdGerente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk1_r");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
