-- Create table
create table P_SAMPLE
(
  fid     VARCHAR2(32) default sys_guid() not null,
  fname   VARCHAR2(50) not null,
  fremark VARCHAR2(200)
)
-- Create/Recreate primary, unique and foreign key constraints 
alter table P_SAMPLE
  add primary key (FID)
;

insert into mp.p_sample (FID, FNAME, FREMARK)
values (sys_guid(), 'name1', 'hello,name1');

insert into mp.p_sample (FID, FNAME, FREMARK)
values (sys_guid(), 'name2', 'hello,name2');

create or replace package proc_samples is

  -- Author  : ADMINISTRATOR
  -- Created : 2017-7-21 10:30:29
  -- Purpose : test excuting procedure using ibatis.net

  --根据fname获取记录，fname不能为空
  procedure proc_sample(AS_XMLPARAM IN VARCHAR,
                        ON_RETURN   out number,
                        OS_MESSAGE  out varchar2,
                        ACUR_TEMP   OUT SYS_REFCURSOR);

end proc_samples;
/
create or replace package body proc_samples is

  --根据fname获取记录，fname不能为空
  procedure proc_sample(AS_XMLPARAM IN VARCHAR,
                        ON_RETURN   OUT number,
                        OS_MESSAGE  OUT varchar2,
                        ACUR_TEMP   OUT SYS_REFCURSOR) is
  begin
    ON_RETURN  := 0;
    OS_MESSAGE := '成功';
    begin
      open ACUR_TEMP for
        select * from mp.p_sample a where a.fname = AS_XMLPARAM;
    exception
      when no_data_found then
        OS_MESSAGE := '没有找到数据';
      when others then
        ON_RETURN  := -1;
        OS_MESSAGE := '查询失败';
    end;
  end proc_sample;
end proc_samples;
/
