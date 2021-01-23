module.exports = async function (context, req,inputDocument) {
    context.log('JavaScript HTTP trigger function processed a request.');

    if(!inputDocument){
        let message="Todo item"+req.query.id+"not found";
        context.log(message);
        //if don find the  item in db  add que storage
        
        context.bindings.outputQueueItem=message;
        context.req={
            status:404,
            body:message
        }

        return;
    }
  
    context.res = {
        // status: 200, /* Defaults to 200 */
        body: inputDocument.desc
    };
}