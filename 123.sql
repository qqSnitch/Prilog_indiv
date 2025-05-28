DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS order_items;
DROP TABLE IF EXISTS products;
DROP TABLE IF EXISTS clients;
DROP TABLE IF EXISTS invoices;
DROP TABLE IF EXISTS invoice_items;

CREATE TABLE clients (
    client_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    address TEXT,
    age INTEGER,
    phone VARCHAR(20) NOT NULL 
);

-- Создаем таблицу товаров
CREATE TABLE products (
    product_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    price DECIMAL(10, 2) NOT NULL,
    unit VARCHAR(20) NOT NULL
);

-- Создаем таблицу заказов
CREATE TABLE orders (
    order_id SERIAL PRIMARY KEY,
    client_id INTEGER REFERENCES clients(client_id) ON DELETE CASCADE,
    order_date DATE NOT NULL DEFAULT CURRENT_DATE,
    total_amount DECIMAL(12, 2) CHECK (total_amount >= 0)
);

-- Создаем таблицу позиций заказа (связь между заказами и товарами)
CREATE TABLE order_items (
    order_item_id SERIAL PRIMARY KEY,
    order_id INTEGER REFERENCES orders(order_id) ON DELETE CASCADE,
    product_id INTEGER REFERENCES products(product_id) ON DELETE CASCADE,
	quantity INTEGER NOT NULL
);

-- Создаем таблицу накладных
CREATE TABLE invoices (
    invoice_id SERIAL PRIMARY KEY,
    invoice_date DATE NOT NULL DEFAULT CURRENT_DATE,
    order_id INTEGER REFERENCES orders(order_id) ON DELETE CASCADE,
	total_amount DECIMAL(12, 2) CHECK (total_amount >= 0)
);

-- Создаем таблицу доставленных товаров (позиции накладной)
CREATE TABLE invoice_items (
    invoice_item_id SERIAL PRIMARY KEY,
    invoice_id INTEGER REFERENCES invoices(invoice_id) ON DELETE CASCADE,
    product_id INTEGER REFERENCES products(product_id) ON DELETE CASCADE,
	quantity INTEGER NOT NULL
);



CREATE OR REPLACE FUNCTION insert_order_info() 
RETURNS TRIGGER AS $ad_fi_trigger$
BEGIN
    -- Обновляем сумму заказа, используя цену из таблицы products
    UPDATE orders 
    SET total_amount = total_amount + (
        SELECT price FROM products WHERE product_id = NEW.product_id)
    WHERE order_id = NEW.order_id;
    
    RETURN NULL;
END;
$ad_fi_trigger$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_order_info() 
RETURNS TRIGGER AS $del_fi_trigger$
BEGIN
    -- Обновляем сумму заказа, используя цену из таблицы products
    UPDATE orders 
    SET total_amount = total_amount - (
        SELECT price FROM products WHERE product_id = OLD.product_id)
    WHERE order_id = OLD.order_id;
    
    RETURN NULL;
END;
$del_fi_trigger$ LANGUAGE plpgsql;



CREATE OR REPLACE FUNCTION insert_invoice_info() 
RETURNS TRIGGER AS $ad_fi_trigger$
BEGIN
    -- Обновляем сумму заказа, используя цену из таблицы products
    UPDATE invoices 
    SET total_amount = total_amount + (
        SELECT price FROM products WHERE product_id = NEW.product_id)
    WHERE invoice_id = NEW.invoice_id;
    
    RETURN NULL;
END;
$ad_fi_trigger$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_invoice_info() 
RETURNS TRIGGER AS $del_fi_trigger$
BEGIN
    -- Обновляем сумму заказа, используя цену из таблицы products
    UPDATE invoices 
    SET total_amount = total_amount - (
        SELECT price FROM products WHERE product_id = OLD.product_id)
    WHERE invoicer_id = OLD.invoice_id;
    
    RETURN NULL;
END;
$del_fi_trigger$ LANGUAGE plpgsql;


CREATE TRIGGER ins_order_info AFTER INSERT ON order_items
FOR EACH ROW EXECUTE PROCEDURE insert_order_info();

CREATE TRIGGER del_order_info AFTER DELETE ON order_items
FOR EACH ROW EXECUTE PROCEDURE delete_order_info();

CREATE TRIGGER ins_invoice_info AFTER INSERT ON invoice_items
FOR EACH ROW EXECUTE PROCEDURE insert_invoice_info();

CREATE TRIGGER del_invoice_info AFTER DELETE ON invoice_items
FOR EACH ROW EXECUTE PROCEDURE delete_invoice_info();

INSERT INTO clients (name, address, age, phone) VALUES
('Иванов Иван Иванович', 'ул. Ленина, 10, кв. 5', 35, '+79161234567'),
('Петров Петр Петрович', 'ул. Гагарина, 25, кв. 12', 28, '+79162345678'),
('Сидорова Анна Сергеевна', 'пр. Мира, 33, кв. 7', 42, '+79163456789'),
('Кузнецов Дмитрий Алексеевич', 'ул. Советская, 15, кв. 3', 31, '+79164567890'),
('Смирнова Елена Владимировна', 'ул. Пушкина, 8, кв. 9', 26, '+79165678901');

-- Вставляем товары
INSERT INTO products (name, price, unit) VALUES
('Ноутбук ASUS', 54990.00, 'шт'),
('Смартфон Xiaomi', 24990.00, 'шт'),
('Наушники Sony', 7990.00, 'шт'),
('Монитор LG', 18990.00, 'шт'),
('Клавиатура Logitech', 2990.00, 'шт'),
('Мышь беспроводная', 1490.00, 'шт'),
('Флешка 32GB', 990.00, 'шт'),
('Внешний HDD 1TB', 4990.00, 'шт'),
('Роутер TP-Link', 3490.00, 'шт'),
('Коврик для мыши', 490.00, 'шт');

-- Вставляем заказы
INSERT INTO orders (client_id, order_date, total_amount) VALUES
(1, '2023-05-10', 0),
(2, '2023-05-11', 0),
(3, '2023-05-12', 0),
(4, '2023-05-13', 0),
(1, '2023-05-14', 0),
(5, '2023-05-15', 0);

-- Вставляем позиции заказов
INSERT INTO order_items (order_id, product_id, quantity) VALUES
(1, 1, 1),  -- Ноутбук ASUS
(1, 3, 1),  -- Наушники Sony
(2, 2, 2),  -- 2 смартфона Xiaomi
(3, 5, 1),  -- Клавиатура Logitech
(3, 6, 1),  -- Мышь беспроводная
(3, 7, 2),  -- 2 флешки 32GB
(4, 4, 1),  -- Монитор LG
(5, 8, 1),  -- Внешний HDD 1TB
(5, 9, 1),  -- Роутер TP-Link
(6, 10, 3); -- 3 коврика для мыши

INSERT INTO invoices (invoice_date, order_id, total_amount) VALUES
('2023-05-11', 1, 0),
('2023-05-12', 2, 0),
('2023-05-13', 3, 0),
('2023-05-16', 5, 0);

INSERT INTO invoice_items (invoice_id, product_id, quantity) VALUES
(1, 1, 1),  -- Ноутбук ASUS
(1, 3, 1),  -- Наушники Sony
(2, 2, 1),  -- 1 смартфон Xiaomi (из 2 заказанных)
(3, 5, 1),  -- Клавиатура Logitech
(3, 6, 1),  -- Мышь беспроводная
(4, 8, 1),  -- Внешний HDD 1TB
(4, 9, 1);  -- Роутер TP-Link