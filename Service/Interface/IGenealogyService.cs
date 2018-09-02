using System.Threading.Tasks;
using System.Collections.Generic;
using historianproductionservice.Model;
using historianproductionservice.Model.Genealogy;

namespace historianproductionservice.Service.Interface  {
    public interface IGenealogyService{
        Task<string> addEndRoll(InputData inputData);        
        Task<List<ProductTraceability>> getProducts(long? startDate, long? endDate, List<ProductTraceability> produtos);
        Task<List<ProductTraceability>> geraListas(int id);
        Task<List<Tool>> getTools(long startDate, long endDate);
        Task<(List<Aco>,List<Liga>)> criaListas(List<ProductTraceability> ligas, List<ProductTraceability> rolo);
        Task<List<Genealogy>> getByOp(string op);
        Task<List<Genealogy>> getByCode(long? startdate, long? endDate, string cod);
        Task<List<Genealogy>> getByDate(long? startdate, long? endDate);
    }

}