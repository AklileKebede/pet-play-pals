import axios from 'axios';

export default{

updateUsernameByUserId(userId, user){

    return axios.put(`/EditUsername/${userId}`, user)
},


}
