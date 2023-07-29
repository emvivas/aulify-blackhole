import axios from 'axios';
axios.defaults.headers.post['Content-Type'] ='application/json;charset=utf-8';
axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';

export const getStatisticsRequest = async (user) => {
    return await axios.get("https://0vnx9rbox9.execute-api.us-east-1.amazonaws.com/api/videogame/stats?userIdentificator="+user.userIdentificator);
}

export const getTotalStatisticsRequest = async () => {
    return await axios.get("https://0vnx9rbox9.execute-api.us-east-1.amazonaws.com/api/videogame/statistics");
}