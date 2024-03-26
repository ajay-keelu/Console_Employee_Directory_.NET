using Models;

namespace Services
{
    public class RoleService
    {
        public JsonService JsonService = new JsonService();

        public Role? GetById(string id)
        {
            try
            {
                return (from role in JsonService.ReadRoles() where role.Id == id select role).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Role> GetAll()
        {
            return JsonService.ReadRoles();
        }

        public bool DeleteById(string Id)
        {
            try
            {
                var Roles = JsonService.ReadRoles().FindAll(role => role.Id != Id);
                if (!JsonService.UpdateRoleJson(Roles)) throw new Exception();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<string> GetRoles()
        {
            return (from role in JsonService.ReadRoles() select role.Id + " " + role.Name.ToUpper()).ToList();
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
                var Roles = JsonService.ReadRoles();
                Roles.Add(role);
                if (!JsonService.UpdateRoleJson(Roles)) throw new Exception();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Role role)
        {
            try
            {
                var Roles = JsonService.ReadRoles();
                int index = Roles.FindIndex(r => r.Id == role.Id);
                Roles[index] = role;
                if (!JsonService.UpdateRoleJson(Roles)) throw new Exception();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public string GenerateId()
        {
            DateTime CurrentDate = DateTime.Now;
            return "TZ" + CurrentDate.Year + CurrentDate.Month + CurrentDate.Day + CurrentDate.Hour + CurrentDate.Minute + CurrentDate.Second;
        }
    }
}