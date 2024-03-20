using MySql.Data.MySqlClient;

namespace GroupProject.Scripts;

class dataBaseConnection{
    public MySqlConnection connection;
    public void Connect(){
        connection = new MySqlConnection("Datasource=127.0.0.1;username=root;password=;database=let_me_show_you");
    }
}
