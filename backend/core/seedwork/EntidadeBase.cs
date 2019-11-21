using System;
using System.ComponentModel.DataAnnotations;

namespace core.seedwork
{
    public interface IEntidadeBase
    {

    }

    public interface IEntidadeBase<T> : IEntidadeBase
    {
        /// <summary>
        /// Identificador
        /// </summary>
        T Id { get; set; }
    }

    public abstract class EntidadeBase<T> : IEntidadeBase<T>
    {
        /// <summary>
        /// Identificador
        /// </summary>
        [Key]
        [Required]
        public T Id { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public bool Ativo { get; set; }

        public static bool operator == (EntidadeBase<T> left, EntidadeBase<T> right)
        {
            if (Equals(left, null))
                return (Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(EntidadeBase<T> left, EntidadeBase<T> right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntidadeBase<T>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = (EntidadeBase<T>)obj;

             return item.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }

    public abstract class EntidadeBase : EntidadeBase<Guid>
    {
        /// <summary>
        /// Desativado
        /// </summary>
        public bool Desativado { get; set; }
    }
}
