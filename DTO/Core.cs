using DTO.Int;


namespace DTO
{
    public abstract class Core : ICore
    {
        public virtual string Id { get; set; }
        public virtual int Version { get; set; }
        public virtual DateTime AddDate { get; set; }
        public virtual string AddDateStr { get { return AddDate.ToLongDateTimeString(); } }
        public virtual string AddUser { get; set; }
        public virtual DateTime? UpdDate { get; set; }
        public virtual string UpdDateStr { get { return UpdDate.ToLongDateTimeString(); } }
        public virtual string UpdUser { get; set; }
        public virtual DateTime ActDate { get; set; }
        public virtual string ActDateStr { get { return ActDate.ToLongDateTimeString(); } }
        public virtual string ActDateStrAgo { get { return ActDate.TimeAgo(); } }
    }
}
