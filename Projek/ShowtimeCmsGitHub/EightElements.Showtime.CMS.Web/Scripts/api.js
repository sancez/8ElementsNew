function getApi(url, data,json) {
    const config ={
        'url': url,
        'type': 'GET',
        'data': data
    }
    if(json){
        config.dataType = 'JSON';
    }
    return $.ajax(config);
}

function postApi(url, data,json) {
    const config ={
        'url': url,
        'type': 'POST',
        'contentType': 'application/json',
        'data': JSON.stringify(data)
    }
    if(json){
        config.dataType = 'JSON';
    }
    return $.ajax(config);
}
async function GetAllEvent() {
    try {
        const data = {
        };
        return await getApi('/Rest/GetAllEvent', data,false);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function GetAllPageText(lang) {
    try {
        const data = {
            lang
        };
        return await getApi('/Rest/GetAllPageText', data,false);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function AddOrUpdatePageText(id,key,text,lang) {
    try {
        const data = {
            id,
            key,
            text,
            lang
        };
        return await postApi('/Rest/AddOrUpdatePageText', data,false);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function GetAllEventParticipant(eventId, eventCategory) {
    try {
        const data = {
            eventId,
            eventCategory
        };
        return await getApi('/Rest/GetAllEventParticipant', data,true);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function GetAllEventParticipantBySearchName(eventId, search, eventCategory) {
    try {
        const data = {
            eventId,
            search,
            eventCategory
        };
        return await getApi('/Rest/GetAllEventParticipantBySearchName', data,true);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function ApproveOrRejectContentDetail(contentDetailId,status) {
    try {
        const data = {
            contentDetailId,
            status
        };
        return await getApi('/Rest/ApproveOrRejectContentDetail', data,false);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function ApproveOrRejectContent(contentId,status) {
    try {
        const data = {
            contentId,
            status
        };
        return await getApi('/Rest/ApproveOrRejectContent', data,false);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function GetAllEventDetailByEventId(eventId) {
    try {
        const data = {
            eventId
        };
        return await getApi('/Rest/GetAllEventDetailByEventId', data,true);
    } catch (e) {
        console.log(e);
        return [];
    }
}

async function GetAllEventDetailByKey(eventId,key) {
    try {
        const data = {
            eventId,
            key
        };
        return await getApi('/Rest/GetEventDetail', data,true);
    } catch (e) {
        console.log(e);
        return [];
    }
}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
