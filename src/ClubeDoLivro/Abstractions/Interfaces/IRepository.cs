using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubeDoLivro.Abstractions.Interfaces
{
	public interface IRepository<TEntity>
	{
		Task<TEntity> ObterPor(int id);
		Task<IEnumerable<TEntity>> ObterTodos();
		Task<TEntity> Incluir(TEntity entity);
		Task<TEntity> Alterar(TEntity entity);
		Task<TEntity> Excluir(TEntity entity);
	}
}