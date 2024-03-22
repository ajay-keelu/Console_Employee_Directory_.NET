using DataBase;

namespace Services
{
    public class RoleService
    {
        public Role? GetById(string id)
        {
            try
            {
                return (from role in GlobalDB.Roles where role.Id == id select role).FirstOrDefault();
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public List<Role> GetAll()
        {
            return GlobalDB.Roles;
        }

        public bool DeleteById(string Id)
        {
            try
            {
                GlobalDB.Roles = GlobalDB.Roles.FindAll(role => role.Id != Id);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<string> GetRoles()
        {
            return (from role in GlobalDB.Roles select role.Id + " " + role.Name.ToUpper()).ToList();
        }

        public bool Save(Role role)
        {
            if (string.IsNullOrEmpty(role.Id)) return this.Create(role);
            else return this.Update(role);
        }

        public bool Create(Role role)
        {
            try
            {
                role.Id = this.GenerateId();
                GlobalDB.Roles.Add(role);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Update(Role role)
        {
            try
            {
                int index = GlobalDB.Roles.FindIndex(r => r.Id == role.Id);
                GlobalDB.Roles[index] = role;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public string GenerateId()
        {
            DateTime CurrentDate = DateTime.Now;
            return "TZ" + CurrentDate.Year + CurrentDate.Month + CurrentDate.Day + CurrentDate.Hour + CurrentDate.Minute + CurrentDate.Second;
        }
    }
}