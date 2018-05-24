-- Create table P_SAMPLE
create table P_SAMPLE
(
  fid     VARCHAR2(32) default sys_guid() not null primary key,
  fname   VARCHAR2(50) not null,
  fremark VARCHAR2(200)
);

-- Create table P_SAMPLE_DETAIL
create table P_SAMPLE_DETAIL
(
  fentity_id NUMBER(12) not null,
  fid        NUMBER(12) not null,
  fvalue     VARCHAR2(50)
);

-- Create table P_SAMPLE_MAIN
create table P_SAMPLE_MAIN
(
  fid      NUMBER(12) not null,
  ftype_id VARCHAR2(32) not null,
  fremark  VARCHAR2(200)
);

-- Create table P_SAMPLE_TYPE
create table P_SAMPLE_TYPE
(
  fid   VARCHAR2(5) not null,
  fname VARCHAR2(50) not null
)

--插入P_SAMPLE数据
insert into mp.p_sample (FID, FNAME, FREMARK)
values (sys_guid(), 'name1', 'hello,name1');
insert into mp.p_sample (FID, FNAME, FREMARK)
values (sys_guid(), 'name2', 'hello,name2');

--插入P_SAMPLE_TYPE数据
insert into P_SAMPLE_TYPE (FID, FNAME)
values ('1', 'test1');
insert into P_SAMPLE_TYPE (FID, FNAME)
values ('2', 'test2');
insert into P_SAMPLE_TYPE (FID, FNAME)
values ('3', 'dev1');
insert into P_SAMPLE_TYPE (FID, FNAME)
values ('4', 'dev2');
insert into P_SAMPLE_TYPE (FID, FNAME)
values ('5', 'pro1');
insert into P_SAMPLE_TYPE (FID, FNAME)
values ('6', 'pro2');
insert into P_SAMPLE_TYPE (FID, FNAME)
values ('7', 'pro3');

--P_SAMPLE_MAIN
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (1, '1', '1');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (2, '1', '2');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (3, '2', '3');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (4, '3', '4');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (5, '4', '5');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (6, '5', '6');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (7, '6', '7');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (8, '7', '8');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (9, '2', '9');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (10, '4', '10');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (11, '5', '11');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (12, '6', '12');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (13, '7', '13');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (14, '5', '14');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (15, '4', '15');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (16, '5', '16');
insert into P_SAMPLE_MAIN (FID, FTYPE_ID, FREMARK)
values (17, '3', '17');

--P_SAMPLE_DETAIL
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (1, 1, '1');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (2, 2, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (3, 3, '3');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (4, 4, '4');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (5, 5, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (6, 6, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (7, 7, '1');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (8, 1, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (9, 2, '1');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (10, 5, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (11, 6, '3');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (12, 7, '3');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (13, 5, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (14, 4, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (15, 3, 'w');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (16, 2, 'w');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (17, 5, '2');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (18, 6, '1');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (19, 4, '1');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (20, 3, '1');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (21, 2, '1');
insert into P_SAMPLE_DETAIL (FENTITY_ID, FID, FVALUE)
values (22, 3, '1');

--存储过程
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
